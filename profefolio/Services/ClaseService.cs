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
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Clase Edit(Clase t)
        {
            throw new NotImplementedException();
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public async Task<Clase> FindById(int id)
        {
            return await _context.Clases.Where(c => !c.Deleted && c.Id == id).FirstOrDefaultAsync();
        }

        public IEnumerable<Clase> GetAll(int page, int cantPorPag)
        {
            return _context.Clases
                    .Where(c => !c.Deleted)
                    .Include(c => c.Ciclo)
                    .ThenInclude(ciclo => ciclo.Nombre)
                    .Include(c => c.Colegio)
                    .ThenInclude(colegio => colegio.Nombre)
                    .Skip(page * cantPorPag).Take(cantPorPag).ToList();
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }
    }
}