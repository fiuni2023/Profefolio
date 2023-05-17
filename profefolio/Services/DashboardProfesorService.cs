using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using profefolio.Helpers;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Models.DTOs.Materia;
using profefolio.Repository;

namespace profefolio.Services
{
    public class DashboardProfesorService : IDashboardProfesor
    {
        public ApplicationDbContext _context;

        public DashboardProfesorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<ColegioProfesor> Add(ColegioProfesor t)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }

        public ColegioProfesor Edit(ColegioProfesor t)
        {
            throw new NotImplementedException();
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public Task<ColegioProfesor> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ColegioProfesor> GetAll(int page, int cantPorPag)
        {
            throw new NotImplementedException();
        }

        public async Task<(Persona, List<Clase>)> GetClasesForCardClases(int idColegio, string emailProfesor = "", int anho = 0)
        {
            var profesor = await _context.Users
                .FirstOrDefaultAsync(a => !a.Deleted
                    && emailProfesor.Equals(a.Email)
                    && a.ColegiosProfesor.Any(c => !c.Deleted && c.ColegioId == idColegio));

            if (profesor == null)
            {
                throw new FileNotFoundException("El usuario no es un profesor");
            }
            var clases = await _context.MateriaListas
                        .Include(a => a.Clase.Ciclo)
                        .Include(a => a.Clase.ClasesAlumnosColegios)
                        .Where(a => !a.Deleted
                            && profesor.Id.Equals(a.ProfesorId)
                            && a.Clase.ColegioId == idColegio
                            && a.Clase.Anho == anho)
                        .Select(a => a.Clase)
                        .ToListAsync();


            return (profesor, clases.DistinctBy(a => a.Id).ToList());
        }

        public async Task<List<ClasesAlumnosColegio>> GetColegioAlumnoId(int idClase, string idProfesor)
        {
            var ClaseAlumnosC = await _context.ClasesAlumnosColegios
                .Join(_context.MateriaListas,
                    cac => cac.ClaseId,
                    ml => ml.ClaseId,
                    (cac, ml) => new { ClasesAlumnosColegio = cac, MateriaLista = ml })
                .Where(a => !a.ClasesAlumnosColegio.Deleted &&
                            a.ClasesAlumnosColegio.ClaseId == idClase &&
                            a.MateriaLista.ProfesorId == idProfesor)
                .Select(a => a.ClasesAlumnosColegio)
                .Distinct()
                .ToListAsync();
            foreach (var item in ClaseAlumnosC)
            {
                Console.WriteLine("*******:" + item.ColegiosAlumnosId);
            }
            return ClaseAlumnosC;
        }



        public async Task<String> FindAlumnoIdByColegioAlumnoId(int idColegiosAlumnos)
        {

            var idAlumno = await _context.ColegiosAlumnos
                    .Where(a => !a.Deleted
                            && a.Id == idColegiosAlumnos)
                    .FirstOrDefaultAsync();
            Console.WriteLine("*****idAlumno: " + idAlumno.PersonaId);
            return idAlumno.PersonaId;
        }
        public async Task<List<string>> FindMateriasOfClase(Persona profesor, int idClase)
        {
            return await _context.MateriaListas.Where(a => !a.Deleted
                        && profesor.Id.Equals(a.ProfesorId)
                        && a.ClaseId == idClase)
                        .Select(a => a.Materia.Nombre_Materia).ToListAsync();
        }

        public async Task<List<MateriaResultFullDTO>> _FindMateriasOfClase(Persona profesor, int idClase)
        {
            return await _context.MateriaListas
                .Where(a => !a.Deleted && profesor.Id.Equals(a.ProfesorId) && a.ClaseId == idClase)
                .Select(a => new MateriaResultFullDTO
                {
                    Id = a.Materia.Id,
                    Nombre_Materia = a.Materia.Nombre_Materia,
                    MateriaListaId = a.Id,
                    MateriaId = a.Materia.Id
                    // Asigna los valores de las propiedades restantes si es necesario
                })
                .ToListAsync();
        }

        public async Task<HorasCatedrasMaterias> FindHorarioMasCercano(Persona profesor, int idClase)
        {
            var horas = await _context.HorasCatedrasMaterias
                        .Where(a => !a.Deleted
                            && a.MateriaLista != null
                            && profesor.Id.Equals(a.MateriaLista.ProfesorId)
                            && a.MateriaLista.ClaseId == idClase)
                        .Include(a => a.HoraCatedra)
                        .ToListAsync();

            horas.Sort((a, b) => TimeComparator.MissingMinutes(DateTime.Now, a.Dia, a.HoraCatedra.Inicio) - TimeComparator.MissingMinutes(DateTime.Now, b.Dia, b.HoraCatedra.Inicio));

            return horas.FirstOrDefault();
        }
        public async Task<HorasCatedrasMaterias> FindHorarioMasCercanoMateria(Persona profesor,
        int idMateriaLista)
        {
            var horas = await _context.HorasCatedrasMaterias
                        .Where(a => !a.Deleted
                            && a.MateriaLista != null
                            && profesor.Id.Equals(a.MateriaLista.ProfesorId)
                            && a.MateriaLista.Id == idMateriaLista)
                        .Include(a => a.HoraCatedra)
                        .ToListAsync();

            if (horas.Any())
            {
                horas.Sort((a, b) => TimeComparator.MissingMinutes(DateTime.Now, a.Dia, a.HoraCatedra.Inicio) - TimeComparator.MissingMinutes(DateTime.Now, b.Dia, b.HoraCatedra.Inicio));

                return horas.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
        public Task Save()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetHorasOfClaseInDay(Persona profesor, int idClase, string dia)
        {
            var duraciones = await _context.HorasCatedrasMaterias
                .Where(a => !a.Deleted
                    && !a.MateriaLista.Deleted
                    && profesor.Id.Equals(a.MateriaLista.ProfesorId)
                    && a.MateriaLista.ClaseId == idClase
                    && dia.ToLower().Equals(a.Dia.ToLower()))
                .Select(a => (DateTime.Parse(a.HoraCatedra.Fin) - DateTime.Parse(a.HoraCatedra.Inicio)).Duration())
                .ToListAsync();

            var horas = new TimeSpan();
            duraciones.ForEach(a =>
            {
                horas += a;
            });
            var hora = horas.Hours > 0 ? $"{horas.Hours}h" : "";
            var minuto = horas.Minutes > 0 ? $"{horas.Minutes}m" : "";
            if (hora.Equals(""))
            {
                return minuto;
            }
            else if (minuto.Equals(""))
            {
                return hora;
            }
            else
            {
                return $"{hora} {minuto}";
            }
        }

        public async Task<string> GetHorasOfMateriaInDay(Persona profesor, int idMateriaLista, string dia)
        {
            var duraciones = await _context.HorasCatedrasMaterias
                .Where(a => !a.Deleted
                    && !a.MateriaLista.Deleted
                    && profesor.Id.Equals(a.MateriaLista.ProfesorId)
                    && a.MateriaLista.Id == idMateriaLista
                    && dia.ToLower().Equals(a.Dia.ToLower()))
                .Select(a => (DateTime.Parse(a.HoraCatedra.Fin) - DateTime.Parse(a.HoraCatedra.Inicio)).Duration())
                .ToListAsync();

            var horas = new TimeSpan();
            duraciones.ForEach(a =>
            {
                horas += a;
            });
            var hora = horas.Hours > 0 ? $"{horas.Hours}h" : "";
            var minuto = horas.Minutes > 0 ? $"{horas.Minutes}m" : "";
            if (hora.Equals(""))
            {
                return minuto;
            }
            else if (minuto.Equals(""))
            {
                return hora;
            }
            else
            {
                return $"{hora} {minuto}";
            }
        }

        public async Task<int> GetEventosOfMateria(string idProfesor, int materia, int idClase)
        {
            List<Evento> duraciones = await _context.Eventos
                .Where(a => !a.Deleted
                    && idProfesor.Equals(a.ProfesorId)
                    && materia.Equals(a.MateriaId)
                    && idClase.Equals(a.ClaseId))
                .ToListAsync();
          
            return duraciones.Count;
        }

        public async Task<List<HorasCatedrasMaterias>> FindAllHorariosClasesByEmailProfesorAndIdColegio(int idColegio, string email, int anho)
        {
            var profesor = await _context.Users
                            .FirstOrDefaultAsync(a => !a.Deleted
                                && email.Equals(a.Email)
                                && a.ColegiosProfesor
                                    .Any(b => !b.Deleted
                                        && b.ColegioId == idColegio));
            if (profesor == null)
            {
                throw new FileNotFoundException("El usuario no fue encontrado.");
            }
            var result = await _context.HorasCatedrasMaterias
                            .Where(a => !a.Deleted
                                && a.MateriaLista != null
                                && !a.MateriaLista.Deleted
                                && !a.MateriaLista.Clase.Deleted
                                && a.MateriaLista.Clase.Anho == anho
                                && profesor.Id.Equals(a.MateriaLista.ProfesorId)
                                && a.MateriaLista.Clase.ColegioId == idColegio)
                            .Include(a => a.HoraCatedra)
                            .Include(a => a.MateriaLista)
                            .Include(a => a.MateriaLista.Clase)
                            .ToListAsync();

            return result;
        }

        public Task<MateriaLista> FindDataForCardOfInfoMateria(int idMateriaLista)
        {
            Console.WriteLine($"\n\n\n\n\n\n\n\n\nHace falta terminar de implementar, ya que falta que se creen todavia las tablas de claificaciones y otros mas parque este completo este servicio \n\n\n\n\n\n\n\n\n");
            
            return _context.MateriaListas
                    .FirstOrDefaultAsync(a => !a.Deleted && a.Id == idMateriaLista);
        }
    }
}