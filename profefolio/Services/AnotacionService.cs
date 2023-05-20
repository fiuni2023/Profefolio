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
    public class AnotacionService : IAnotacion
    {
        private ApplicationDbContext _context;
        public AnotacionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Anotacion> Add(Anotacion t)
        {
            var result = await _context.Anotacion.AddAsync(t);
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

        public Anotacion Edit(Anotacion t)
        {
            _context.Entry(t).State = EntityState.Modified;
            return t;
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public Task<Anotacion> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Anotacion>> GetAll()
        {
            return await _context.Anotacion.Where(a=> !a.Deleted).ToArrayAsync();
        
        }

        public IEnumerable<Anotacion> GetAll(int page, int cantPorPag)
        {
            return _context.Anotacion.Where(a => !a.Deleted).Skip(page * cantPorPag).Take(cantPorPag).ToList();
        
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}