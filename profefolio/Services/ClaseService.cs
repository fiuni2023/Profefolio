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
    public class ClaseService : IClase
    {
        private ApplicationDbContext _context;
        public ClaseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Clase> Add(Clase t)
        {
            var result = await _context.Clases.AddAsync(t);
            return result.Entity;
        }

        public int Count()
        {
            return _context.Clases.Count(c => !c.Deleted);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Clase Edit(Clase t)
        {
            _context.Entry(t).State = EntityState.Modified;
            return t;
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public async Task<Clase> FindById(int id)
        {
            return await _context.Clases
                        .Where(c => !c.Deleted && c.Id == id)
                        .Include(c => c.Ciclo)
                        .Include(c => c.Colegio)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Clase>> GetAllByIdColegio(int page, int cantPorPag, int idColegio)
        {
            return await _context.Clases
                    .Where(c => !c.Deleted && c.ColegioId == idColegio)
                    .Include(c => c.Ciclo)
                    .Include(c => c.Colegio)
                    .Skip(page * cantPorPag).Take(cantPorPag)
                    .OrderByDescending(c => c.Id).ToListAsync();
        }

        public async Task<IEnumerable<Clase>> GetByIdColegio(int idColegio)
        {
            return await _context.Clases.Where(c => !c.Deleted && c.ColegioId == idColegio)
            .Include(c => c.Ciclo)
            .OrderByDescending(clase => clase.Id)
            .ToListAsync();
        }


        public Task Save()
        {
            return _context.SaveChangesAsync();
        }

        IEnumerable<Clase> IRepository<Clase>.GetAll(int page, int cantPorPag)
        {
            throw new NotImplementedException();
        }
    }
}