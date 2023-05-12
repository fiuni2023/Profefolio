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
            var profesor = await _context.Users
                .FirstOrDefaultAsync(a => !a.Deleted
                    && emailProfesor.Equals(a.Email)
                    && a.ColegiosProfesor.Any(c => !c.Deleted && c.ColegioId == idColegio));

            if (profesor == null)
            {
                throw new FileNotFoundException("El usuario no es un profesor");
            }
            var clases = await _context.MateriaListas
                        .Where(a => !a.Deleted
                            && profesor.Id.Equals(a.ProfesorId)
                            && a.Clase.ColegioId == idColegio
                            && a.Clase.Anho == anho)
                        .Include(a => a.Clase.Ciclo)
                        .Include(a => a.Clase.ClasesAlumnosColegios)
                        .Include(a => a.Clase.MateriaListas)
                        .Select(a => a.Clase)
                        .ToListAsync();

            clases.ForEach(async a =>
            {
                a.MateriaListas = await _context.MateriaListas
                    .Include(w => w.Materia)
                    .Where(b => b.ClaseId == a.Id && profesor.Id.Equals(b.ProfesorId))
                    .ToListAsync();
            });

            return clases;
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

    }
}