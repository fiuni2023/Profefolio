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
    public class CicloService : ICiclo
    {
        private ApplicationDbContext _context;

        public CicloService(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<Ciclo> Add(Ciclo t)
        {
            var result = await _context.Ciclos.AddAsync(t);
            return result.Entity;
        }

        public int Count()
        {
            return _context.Ciclos.Where(c => !c.Deleted).Count();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Ciclo Edit(Ciclo t)
        {
            _context.Entry(t).State = EntityState.Modified;
            return t;
        }

        public Task<bool> ExisitNombre(string nombre = "")
        {
            return _context.Ciclos
                        .Where(c => nombre.ToLower().Equals(c.Nombre != null ? c.Nombre.ToLower() : "")
                                    && !c.Deleted).AnyAsync();
        }

        public async Task<bool> ExisitOther(int id, string nombre = "")
        {
            return await _context.Ciclos
                        .Where(c => nombre.ToLower().Equals(c.Nombre != null ? c.Nombre.ToLower() : "")
                                    && !c.Deleted
                                    && c.Id != id).AnyAsync();
        }

        public bool Exist()
        {
            return _context.Ciclos.Where(c => !c.Deleted).Any();
        }

        public async Task<Ciclo> FindById(int id)
        {
            return await _context.Ciclos.Where(c => !c.Deleted && c.Id == id).FirstOrDefaultAsync();
        }

        public IEnumerable<Ciclo> GetAll(int page, int cantPorPag)
        {
            return _context.Ciclos.Where(c => !c.Deleted).Skip(page * cantPorPag).Take(cantPorPag).ToList();
        }

        public async Task<IEnumerable<Ciclo>> GetAll()
        {
            return await _context.Ciclos.Where(c => !c.Deleted).ToArrayAsync();
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }
    }
}