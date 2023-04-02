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
    public class ColegioProfesorService : IColegioProfesor
    {
        public ApplicationDbContext _context;
        public ColegioProfesorService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ColegioProfesor> Add(ColegioProfesor t)
        {
            var result = await _context.ColegiosProfesors.AddAsync(t);
            return result.Entity;
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public async Task<int> Count(int idColegio)
        {
            return await _context.ColegiosProfesors
                            .CountAsync(ca => !ca.Deleted && ca.ColegioId == idColegio);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public ColegioProfesor Edit(ColegioProfesor t)
        {
            throw new NotImplementedException();
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exist(string idProf, int idColegio)
        {
            return await _context.ColegiosProfesors
                    .AnyAsync(ca => !ca.Deleted
                        && ca.PersonaId != null
                        && ca.ColegioId == idColegio
                        && ca.PersonaId.Equals(idProf));
        }

        public Task<IEnumerable<ColegioProfesor>> FindAllByIdColegio(int page, int cantPorPag, int idColegio)
        {
            throw new NotImplementedException();
        }

        public Task<ColegioProfesor> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ColegioProfesor> GetAll(int page, int cantPorPag)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }
    }
}