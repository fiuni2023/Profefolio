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
    public class ColegiosAlumnosServices : IColegiosAlumnos
    {
        private ApplicationDbContext _context;
        public ColegiosAlumnosServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ColegiosAlumnos> Add(ColegiosAlumnos t)
        {
            var result = await _context.ColegiosAlumnos.AddAsync(t);
            return result.Entity;
        }

        /*
            Retorna la cantidad de alumnos de un colegio
        */
        public async Task<int> Count(int idColegio)
        {
            return await _context.ColegiosAlumnos
                            .CountAsync(ca => !ca.Deleted && ca.ColegioId == idColegio);
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public ColegiosAlumnos Edit(ColegiosAlumnos t)
        {
            _context.Entry(t).State = EntityState.Modified;
            return t;
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exist(string idAlumno, int idColegio)
        {
            return await _context.ColegiosAlumnos.AnyAsync(ca => !ca.Deleted
                    && ca.ColegioId == idColegio
                    && ca.PersonaId.Equals(idAlumno));
        }

        public async Task<IEnumerable<ColegiosAlumnos>> FindAllByIdColegio(int page, int cantPorPag, int idColegio)
        {
            return await _context.ColegiosAlumnos
                    .Where(ca => !ca.Deleted && ca.ColegioId == idColegio)
                    .OrderByDescending(ca => ca.Id)
                    .Skip(page * cantPorPag)
                    .Take(page).ToListAsync();
        }

        public async Task<IEnumerable<ColegiosAlumnos>> FindAllNoAssignedToClaseByEmailAdminAndIdClase(string adminEmail, int idClase)
        {
            return await _context.ColegiosAlumnos
                            .Where(ca => !ca.Deleted 
                                && ca.Colegio.personas.Email.Equals(adminEmail)
                                || (ca.ClasesAlumnosColegios == null 
                                && ca.ClasesAlumnosColegios.Any(a => !a.Deleted && a.ClaseId != idClase)))
                            .Include(a => a.Persona)
                            .ToListAsync();
        }

        public async Task<ColegiosAlumnos> FindById(int id)
        {
            return await _context.ColegiosAlumnos
                .Include(a => a.Colegio)
                .Include(a => a.Colegio.personas)
                .FirstOrDefaultAsync(ca => !ca.Deleted && ca.Id == id);
        }

        public IEnumerable<ColegiosAlumnos> GetAll(int page, int cantPorPag)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }
    }
}