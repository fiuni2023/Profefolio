using Microsoft.EntityFrameworkCore;
using profefolio.Helpers;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Models.DTOs.Materia;
using profefolio.Models.DTOs.DashboardProfesor.GetWithOpcions;
using profefolio.Models.DTOs.DashboardPuntajes;
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

        public Task<int> GetEventosOfMateria(string idProfesor, int materia, int idClase)
        {
            /*
            List<Evento> duraciones = await _context.Eventos
                .Where(a => !a.Deleted
                    && idProfesor.Equals(a.ProfesorId)
                    && materia.Equals(a.MateriaId)
                    && idClase.Equals(a.ClaseId))
                .ToListAsync();

            return duraciones.Count;
            */
            return Task.FromResult(0);
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

        /*
        Obtener:
        {
		///			anotaciones: 25,
		///			calificaciones:	4, //numero de evaluaciones en esa materia
		///			asistencias: 8,    //porcentaje asistencias en esa materia
		///			documentos: 4
		///		}    
        */
        public async Task<DBCardsMateriaInfo> FindDataForCardOfInfoMateria(int idMateriaLista, string emailProfesor)
        {
            var materia = await _context.MateriaListas
                .Include(a => a.Profesor)
                .FirstOrDefaultAsync(a => !a.Deleted && a.Id == idMateriaLista && emailProfesor.Equals(a.Profesor.Email));

            if (materia == null)
            {
                throw new FileNotFoundException("La materia no fue encontrada.");
            }

            var dbCardsMateriaInfo = new DBCardsMateriaInfo();

            // Obtener el número de anotaciones del prf
            dbCardsMateriaInfo.Anotaciones = await _context.Anotaciones
                .CountAsync(a => a.MateriaListaId == idMateriaLista && a.CreatedBy == emailProfesor && !a.Deleted);


            // Obtener el número total de evaluaciones (calificaciones)
            dbCardsMateriaInfo.Calificaciones = await _context.Eventos
                .CountAsync(e => e.MateriaListaId == idMateriaLista && e.CreatedBy == emailProfesor && !e.Deleted);

            /*
            Para calcular el porcentaje de presentes en la materia
            obtener tanto el número total de presentes como el número total de registros
            en la tabla Asistencias para la materia específica. 
            */
            var totalPresentes = await _context.Asistencias
            .CountAsync(a => a.MateriaListaId == idMateriaLista && a.Estado == 'P' && !a.Deleted);

            var totalRegistros = await _context.Asistencias
                .CountAsync(a => a.MateriaListaId == idMateriaLista && !a.Deleted);

            double porcentajePresentes = 0;
            if (totalRegistros > 0)
            {
                porcentajePresentes = (double)totalPresentes / totalRegistros * 100;
            }

            dbCardsMateriaInfo.Asistencias = Math.Round(porcentajePresentes, 2);

            // Obtener el número de documentos
            dbCardsMateriaInfo.Documentos = await _context.Documentos
                .CountAsync(d => d.MateriaListaId == idMateriaLista && d.CreatedBy == emailProfesor && !d.Deleted);

            return dbCardsMateriaInfo;
        }




        public async Task<MateriaLista> GetPromediosPuntajesByIdMateriaLista(int idMateriaLista, string emailProfesor)
        {
            /*
                TODO 
                AGREGAR LA IMPLEMENTACION PARA OBTENER EL PROMEDIO DE LOS PUNTAJES CUANDO SE AGREGUE LA TABLA DE CALIFICACIONES
            */
            var result = await _context.MateriaListas.Include(a => a.Profesor).FirstOrDefaultAsync(a => !a.Deleted && a.Id == idMateriaLista && emailProfesor.Equals(a.Profesor.Email));

            if (result == null)
            {
                throw new FileNotFoundException("La materia no fue encontrada.");
            }
            return result;
        }

        public async Task<(double, double, double)> GetPromedioAsistenciasByMonth(int year, int month, int idMateriaLista, string profesorId)
        {
            var fechasAsistencias = await _context.Asistencias
                                    .Include(a => a.MateriaLista)
                                    .Where(a => !a.Deleted
                                        && a.MateriaListaId == idMateriaLista
                                        && a.Fecha.Year == year
                                        && a.Fecha.Month == month
                                        && profesorId.Equals(a.MateriaLista.ProfesorId))
                                        .Select(a => new
                                        {
                                            AlumnoId = a.ClasesAlumnosColegioId,
                                            Fecha = a.Fecha,
                                            Estado = a.Estado
                                        })
                                        .GroupBy(a => a.Fecha)

                                        .Select(a => new
                                        {
                                            Fecha = a.Key,
                                            Total = a.Count(),
                                            AsistenciasGrupo = a.GroupBy(b => b.Estado).Select(c => new
                                            {
                                                Estado = c.Key,
                                                Cantidad = c.Count()
                                            })
                                        })
                                        .Select(a => a.AsistenciasGrupo)
                                    .ToListAsync();


            var cantidadTotalPresentes = 0;
            var cantidadTotalAusentes = 0;
            var cantidadTotalJustificados = 0;

            foreach (var asistenciasGrupo in fechasAsistencias)
            {
                foreach (var asist in asistenciasGrupo)
                {
                    if (asist.Estado == 'P')
                    {
                        cantidadTotalPresentes += asist.Cantidad;
                    }
                    else if (asist.Estado == 'A')
                    {
                        cantidadTotalAusentes += asist.Cantidad;
                    }
                    else if (asist.Estado == 'J')
                    {
                        cantidadTotalJustificados += asist.Cantidad;
                    }
                }
            }


            if (fechasAsistencias.Count == 0)
            {
                return (0, 0, 0);
            }

            return (
                        Math.Round(((double)cantidadTotalPresentes / fechasAsistencias.Count), 2),
                        Math.Round(((double)cantidadTotalAusentes / fechasAsistencias.Count), 2),
                        Math.Round(((double)cantidadTotalJustificados / fechasAsistencias.Count), 2)
                    );
        }
        /*
        //Un evento tiene: tipo, fecha, materia,clase, colegio
        //Una evaluacion tiene: tipo, fecha, materiaListaId
        //MateriaLista tiene: claseId, MateriaId, ProfesorId
        //Clase tiene: colegioId

        * Buscar ClaseId,ProfesorId y MateriaId desde la propiedad MateriaListaId de la tabla Evaluaciones.
        * Filtrar todos los datos en base a que idColegio == ColegioId de la tabla Clases a partir de ClaseId que se 
          obtuvo previamente.
        * Retornar una lista de DBCardEventosClaseDTO que contenga el tipo, fecha, materia, clase y colegio
          donde idProfesor == ProfesorId
        */
        public async Task<List<DBCardEventosColegioDTO>> FindEventosOfClase(string idProfesor, int idColegio)
        {
            var clases = await _context.Clases
                .Where(c => c.ColegioId == idColegio)
                .Include(c => c.Colegio)
                .ToListAsync();

            var claseIds = clases.Select(c => c.Id).ToList();

            var materiaListas = await _context.MateriaListas
                .Where(m => m.ProfesorId == idProfesor && claseIds.Contains(m.ClaseId))
                .Include(m => m.Materia)
                .ToListAsync();

            var materiaListaIds = materiaListas.Select(m => m.Id).ToList();

            var eventos = await _context.Eventos
                .Where(e => materiaListaIds.Contains(e.MateriaListaId) && !e.Deleted)
                .ToListAsync();

            var eventosClase = eventos.Select(e => new DBCardEventosColegioDTO
            {
                Tipo = e.Tipo,
                Fecha = e.Fecha,
                nombreMateria = materiaListas.FirstOrDefault(m => m.Id == e.MateriaListaId)?.Materia?.Nombre_Materia,
                nombreClase = clases.FirstOrDefault(c => c.Id == materiaListas.FirstOrDefault(m => m.Id == e.MateriaListaId)?.ClaseId)?.Nombre
            }).ToList();

            return eventosClase;
        }

        public async Task<List<DBCardEventosClaseDTO>> FindEventosOfClase(string idProfesor)
        {
            var clases = await _context.Clases
                .Include(c => c.Colegio)
                .ToListAsync();

            var claseIds = clases.Select(c => c.Id).ToList();

            var materiaListas = await _context.MateriaListas
                .Where(m => m.ProfesorId == idProfesor && claseIds.Contains(m.ClaseId))
                .Include(m => m.Materia)
                .ToListAsync();

            var materiaListaIds = materiaListas.Select(m => m.Id).ToList();

            var eventos = await _context.Eventos
                .Where(e => materiaListaIds.Contains(e.MateriaListaId))
                .ToListAsync();

            var eventosClase = eventos.Select(e => new DBCardEventosClaseDTO
            {
                Tipo = e.Tipo,
                Fecha = e.Fecha,
                nombreMateria = materiaListas.FirstOrDefault(m => m.Id == e.MateriaListaId)?.Materia?.Nombre_Materia,
                nombreClase = clases.FirstOrDefault(c => c.Id == materiaListas.FirstOrDefault(m => m.Id == e.MateriaListaId)?.ClaseId)?.Nombre,
                nombreColegio = clases.FirstOrDefault(c => c.Id == materiaListas.FirstOrDefault(m => m.Id == e.MateriaListaId)?.ClaseId)?.Colegio?.Nombre
            }).ToList();

            return eventosClase;
        }

        public async Task<List<DBCardEventosMateriaDTO>> FindEventosMaterias(string idProfesor, int idClase)
        {
            var clases = await _context.Clases
                .Where(c => c.Id == idClase)
                .Include(c => c.Colegio)
                .ToListAsync();

            var claseIds = clases.Select(c => c.Id).ToList();

            var materiaListas = await _context.MateriaListas
                .Where(m => m.ProfesorId == idProfesor && claseIds.Contains(m.ClaseId))
                .Include(m => m.Materia)
                .ToListAsync();

            var materiaListaIds = materiaListas.Select(m => m.Id).ToList();

            var eventos = await _context.Eventos
                .Where(e => materiaListaIds.Contains(e.MateriaListaId))
                .ToListAsync();

            var eventosClase = eventos.Select(e => new DBCardEventosMateriaDTO
            {
                Tipo = e.Tipo,
                Fecha = e.Fecha,
                nombreMateria = materiaListas.FirstOrDefault(m => m.Id == e.MateriaListaId)?.Materia?.Nombre_Materia
            }).ToList();

            return eventosClase;
        }

        public async Task<List<DashboardPuntajeDTO>> ShowPuntajes(string user, int idMateriaLista)
        {

            var validateMateria = await _context.MateriaListas
                .Include(x => x.Profesor)
                .AnyAsync(ml => !ml.Deleted
                             && ml.Id == idMateriaLista
                             && ml.Profesor.Email.Equals(user));

            if (!validateMateria)
            {
                throw new UnauthorizedAccessException();
            }


            var queryEvaluaciones = await _context.Eventos
                .Where(e => !e.Deleted && e.MateriaListaId == idMateriaLista)
                .Include(ev => ev.EvaluacionAlumnos)
                .ToListAsync();

            var result = new List<DashboardPuntajeDTO>();

            foreach (var evaluacion in queryEvaluaciones)
            {

                var porcentajesSum = evaluacion.EvaluacionAlumnos
                    .Where(x => !x.Deleted)
                    .Sum(y => y.PorcentajeLogrado);

                var cant = evaluacion.EvaluacionAlumnos
                    .Count(x => !x.Deleted);

                var prom = porcentajesSum / cant;
                var dashPuntaje = new DashboardPuntajeDTO
                {
                    Promedio = prom,
                    NombreEvaluacion = evaluacion.Nombre,
                    Tipo = evaluacion.Tipo
                };


                result.Add(dashPuntaje);
            }

            return result;
        }
    }
}