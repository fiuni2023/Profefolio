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


        public Task<HoraCatedra> Add(HoraCatedra t)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public HoraCatedra Edit(HoraCatedra t)
        {
            throw new NotImplementedException();
        }

        public bool Exist()
        {
            throw new NotImplementedException();
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

        public Task Save()
        {
            throw new NotImplementedException();
        }
    }
}