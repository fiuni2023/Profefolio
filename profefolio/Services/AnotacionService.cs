using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;
using Microsoft.EntityFrameworkCore;

namespace profefolio.Services
{
    public class AnotacionesService : IAnotacion
    {
        private ApplicationDbContext _context;

        public AnotacionesService(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<Anotacion> Add(Anotacion t)
        {
            var result = await _context.Anotaciones.AddAsync(t);
            return result.Entity;
        }

        public int Count()
        {
            return _context.Anotaciones.Where(c => !c.Deleted).Count();
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
            return _context.Anotaciones.Where(c => !c.Deleted).Any();
        }

        public async Task<Anotacion> FindById(int id)
        {
            return await _context.Anotaciones.Where(c => !c.Deleted && c.Id == id).FirstOrDefaultAsync();
        }

        public IEnumerable<Anotacion> GetAll(int page, int cantPorPag)
        {
            return _context.Anotaciones.Where(c => !c.Deleted).Skip(page * cantPorPag).Take(cantPorPag).ToList();
        }

        public async Task<IEnumerable<Anotacion>> GetAll()
        {
            return await _context.Anotaciones.Where(c => !c.Deleted).ToArrayAsync();
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }
    }
}