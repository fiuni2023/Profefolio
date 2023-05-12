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

        public async Task<IEnumerable<Clase>> GetClasesForCardClases(int idColegio, string emailProfesor = "", int anho = 0)
        {
            var profesor = await _context.Users.FirstOrDefaultAsync(a => emailProfesor.Equals(a.Email));

            if(profesor == null){
                throw new FileNotFoundException("El usuario no es un profesor");
            }

            return await _context.Clases
                                .Where(c => !c.Deleted 
                                    && c.Anho == anho 
                                    && c.ColegioId == idColegio
                                    && c.Colegio != null
                                    && !c.Colegio.Deleted
                                    && c.Colegio.ColegioProfesores.Any(cp => !cp.Deleted
                                        && cp.Persona != null
                                        && !cp.Persona.Deleted 
                                        && profesor.Id.Equals(cp.PersonaId))
                                    && c.MateriaListas != null
                                    && c.MateriaListas.Any(mt => !mt.Deleted && profesor.Email.Equals(mt.ProfesorId)))
                                .Include(c => c.Ciclo)
                                .Include(c => c.MateriaListas)
                                .Include(c => c.ClasesAlumnosColegios).ToListAsync();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }
        
    }
}