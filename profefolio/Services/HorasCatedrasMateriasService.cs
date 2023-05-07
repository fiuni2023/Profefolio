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
    public class HorasCatedrasMateriasService : IHorasCatedrasMaterias
    {
        private ApplicationDbContext _context;

        public HorasCatedrasMateriasService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HorasCatedrasMaterias> Add(HorasCatedrasMaterias t)
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

        public HorasCatedrasMaterias Edit(HorasCatedrasMaterias t)
        {
            throw new NotImplementedException();
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public Task<HorasCatedrasMaterias> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HorasCatedrasMaterias> GetAll(int page, int cantPorPag)
        {
            throw new NotImplementedException();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}