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
            _context.Entry(t).State = EntityState.Added;
            return await Task.FromResult(t);
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

        public async Task<IEnumerable<ColegioProfesor>> FindAllByIdColegio(int page, int cantPorPag, int idColegio)
        {
            return await _context.ColegiosProfesors
                    .Where(cp => !cp.Deleted && cp.ColegioId == idColegio)
                    .OrderByDescending(cp => cp.Id)
                    .Skip(page * cantPorPag)
                    .Take(cantPorPag)
                    .Include(cp => cp.Persona)
                    .Include(cp => cp.Colegio)
                    .ToListAsync();
        }

        public async Task<IEnumerable<ColegioProfesor>> FindAllByIdColegio(int idColegio)
        {
            return await _context.ColegiosProfesors
                    .Where(cp => !cp.Deleted && cp.ColegioId == idColegio)
                    .Include(cp => cp.Persona)
                    .ToListAsync();
        }

        public async Task<ColegioProfesor> FindById(int id)
        {
            return await _context
                .ColegiosProfesors
                .Where(cp => cp != null && !cp.Deleted && cp.Id == id)
                .Include(cp => cp.Persona)
                .Include(cp => cp.Colegio)
                .FirstOrDefaultAsync();
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