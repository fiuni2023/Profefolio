using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services
{
    public class ColegioProfesorService : IColegioProfesor
    {
        public ApplicationDbContext _context;
        public ColegioProfesorService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ColegioProfesor> Add(ColegioProfesor t)
        {
            _context.Entry(t).State = EntityState.Added;
            return await Task.FromResult(t);
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public async Task<int> Count(int idColegio, string userEmail)
        {
            return await _context.ColegiosProfesors
                            .CountAsync(cp => !cp.Deleted 
                                && cp.ColegioId == idColegio
                                && cp.Persona != null 
                                && cp.Colegio != null 
                                && (userEmail.Equals(cp.Colegio.personas.Email) 
                                    || userEmail.Equals(cp.Persona.Email)));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public ColegioProfesor Edit(ColegioProfesor t)
        {
            _context.Entry(t).State = EntityState.Modified;
            return t;
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exist(string idProf, int idColegio)
        {
            return await _context.ColegiosProfesors
                    .AnyAsync(ca => !ca.Deleted
                        && ca.PersonaId != null
                        && ca.ColegioId == idColegio
                        && ca.PersonaId.Equals(idProf));
        }

        public async Task<bool> Exist(int idColegio)
        {
            return await _context.ColegiosProfesors
                    .AnyAsync(ca => !ca.Deleted
                        && ca.PersonaId != null
                        && ca.ColegioId == idColegio);
        }

        public async Task<bool> Exist(string idProfesor, string emailAdmin)
        {
            return await _context.ColegiosProfesors.AnyAsync(cp =>
                !cp.Deleted 
                && cp.Colegio != null
                && cp.PersonaId != null
                && cp.Persona != null
                && !cp.Persona.Deleted
                && !cp.Colegio.Deleted
                && !cp.Colegio.personas.Deleted
                && cp.PersonaId.Equals(idProfesor) 
                && cp.Colegio.personas.Email.Equals(emailAdmin));
        }

        public async Task<IEnumerable<ColegioProfesor>> FindAllByIdColegio(int page, int cantPorPag, int idColegio, string userEmail)
        {
            return await _context.ColegiosProfesors
                    .Where(cp => !cp.Deleted 
                        && cp.ColegioId == idColegio
                        && cp.Colegio != null
                        && cp.Persona != null
                        && (userEmail.Equals(cp.Colegio.personas.Email) 
                            || userEmail.Equals(cp.Persona.Email)))
                    .OrderByDescending(cp => cp.Id)
                    .Skip(page * cantPorPag)
                    .Take(cantPorPag)
                    .Include(cp => cp.Persona)
                    .Include(cp => cp.Colegio)
                    .ToListAsync();
        }

        public async Task<IEnumerable<ColegioProfesor>> FindAllByIdColegio(int idColegio, string userEmail)
        {
            return await _context.ColegiosProfesors
                    .Where(cp => !cp.Deleted 
                        && cp.ColegioId == idColegio
                        && cp.Colegio != null
                        && cp.Persona != null
                        && (userEmail.Equals(cp.Colegio.personas.Email) 
                            || userEmail.Equals(cp.Persona.Email)))
                    .Include(cp => cp.Persona)
                    .ToListAsync();
        }

        public async Task<(List<ColegioProfesor>, List<Clase>, List<HorasCatedrasMaterias>)> FindAllClases(string emailProfesor = "")
        {
            /*
                se buscan los colegios en donde el profesor tenga clases en el anho actual y 
                los horarios que correspondan a las clases del anho actual
            */
            var colegios = await _context.ColegiosProfesors
                    .Where(a => !a.Deleted 
                        && emailProfesor.Equals(a.Persona.Email)
                        && a.Colegio.ListaClases.Where(b => !b.Deleted && b.Anho == DateTime.Now.Year).Any())
                    .Include(a => a.Colegio)
                    .Include(a => a.Colegio.ListaClases)
                    .ToListAsync();

            var clases = await _context.Clases
                .Where(a => !a.Deleted 
                    && a.Colegio.ColegioProfesores.Any(c => !c.Deleted && emailProfesor.Equals(c.Persona.Email))
                    && a.MateriaListas.Any(m => !m.Deleted && !m.Profesor.Deleted && emailProfesor.Equals(m.Profesor.Email)))
                .ToListAsync();

            var horarios = await _context.HorasCatedrasMaterias
                    .Where(a => !a.Deleted 
                        && !a.MateriaLista.Deleted 
                        && !a.MateriaLista.Profesor.Deleted 
                        && emailProfesor.Equals(a.MateriaLista.Profesor.Email)
                        //&& clases.Any(c => c.Id == a.MateriaLista.ClaseId)
                        && a.MateriaLista.Clase.Anho == DateTime.Now.Year)
                    .Include(a => a.HoraCatedra)
                    .Include(a => a.MateriaLista)
                    .ToListAsync();
                
                    return (colegios, clases, horarios);
                
            
            throw new FileNotFoundException("No se tienen Clases asignadas en ningun Colegio.");
        }

        public async Task<ColegioProfesor> FindById(int id)
        {
            return await _context
                .ColegiosProfesors
                .Where(cp => cp != null && !cp.Deleted && cp.Id == id)
                .Include(cp => cp.Persona)
                .Include(cp => cp.Colegio)
                .Include(cp => cp.Colegio.personas)
                .FirstOrDefaultAsync();
        }

        public IEnumerable<ColegioProfesor> GetAll(int page, int cantPorPag)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }
    }
}