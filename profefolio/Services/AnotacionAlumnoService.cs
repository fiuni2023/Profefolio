using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using profefolio.Models.Entities;
using profefolio.Models;
using profefolio.Repository;

namespace profefolio.Services
{
    public class AnotacionAlumnoService : IAnotacionAlumno
    {

        private readonly ApplicationDbContext _context;

        public AnotacionAlumnoService(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<AnotacionAlumno> Add(AnotacionAlumno t)
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

        public AnotacionAlumno Edit(AnotacionAlumno t)
        {
            _context.Entry(t).State = EntityState.Modified;
            return t;
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public Task<AnotacionAlumno> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AnotacionAlumno> GetAll(int page, int cantPorPag)
        {
            throw new NotImplementedException();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}