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
    public class HoraCatedraService : IHoraCatedra
    {
        private ApplicationDbContext _context;

        public HoraCatedraService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<HoraCatedra> Add(HoraCatedra t)
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

        public HoraCatedra Edit(HoraCatedra t)
        {
            throw new NotImplementedException();
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exist(string inicio = "", string fin = "")
        {
            return await _context.HorasCatedras.AnyAsync(h => !h.Deleted && inicio.Equals(h.Inicio) && fin.Equals(h.Fin));
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.HorasCatedras.AnyAsync(a => !a.Deleted && a.Id == id);
        }

        public async Task<List<HoraCatedra>> FindAll()
        {
            return await _context.HorasCatedras.Where(a => !a.Deleted).ToListAsync();
        }

        public Task<HoraCatedra> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HoraCatedra> GetAll(int page, int cantPorPag)
        {
            throw new NotImplementedException();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}