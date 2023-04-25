using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;
using Microsoft.EntityFrameworkCore;

namespace profefolio.Services
{
    public class ClasesAlumnosColegioService : IClasesAlumnosColegio
    {
        private ApplicationDbContext _context;
        public ClasesAlumnosColegioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ClasesAlumnosColegio> Add(ClasesAlumnosColegio t)
        {
            _context.Entry(t).State = EntityState.Added;
            return await Task.FromResult(t);
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }

        public ClasesAlumnosColegio Edit(ClasesAlumnosColegio t)
        {
            _context.Entry(t).State = EntityState.Modified;
            return t;
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exist(int ClaseId, int ColegioAlumnoId)
        {
            return await _context.ClasesAlumnosColegios
                .AnyAsync(a => !a.Deleted
                && a.ClaseId == ClaseId
                && a.ColegiosAlumnosId == ColegioAlumnoId);
        }

        public async Task<ClasesAlumnosColegio> FindById(int id)
        {
            return await _context.ClasesAlumnosColegios
                    .Where(a => !a.Deleted && a.Id == id)
                    .Include(a => a.Clase)
                    .Include(a => a.ColegiosAlumnos)
                    .Include(a => a.ColegiosAlumnos.Persona)
                    .FirstOrDefaultAsync();
        }

        public IEnumerable<ClasesAlumnosColegio> GetAll(int page, int cantPorPag)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }
    }
}