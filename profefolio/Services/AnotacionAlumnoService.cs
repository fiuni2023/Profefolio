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

        public async Task<List<AnotacionAlumno>> GetAllByAlumnoIdAndMateriaListaId(int idAlumno, int idMateriaLista, string profesorEmail)
        {
            var profesor = await _context.Users.FirstOrDefaultAsync(a => !a.Deleted && profesorEmail.Equals(a.Email));
            if(profesor == null){
                throw new FileNotFoundException("Profesor invalido");
            }
            return await _context.AnotacionesAlumnos
                .Where(a => !a.Deleted
                        && a.AlumnoId == idAlumno
                        && a.MateriaListaId == idMateriaLista
                        && a.MateriaLista.ProfesorId == profesor.Id)
                .ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidarDatos(int idMateriaLista, string emailProfesor, int idAlumno)
        {
            var materialista = await _context.MateriaListas
                .Include(a => a.Profesor)
                .Include(a => a.Clase)
                .FirstOrDefaultAsync(a => !a.Deleted 
                    && a.Id == idMateriaLista 
                    && emailProfesor.Equals(a.Profesor.Email));
            if(materialista == null){
                return false;
            }

            var alumno = await _context.ClasesAlumnosColegios
                .FirstOrDefaultAsync(a => !a.Deleted
                    && a.Id == idAlumno
                    && a.ClaseId == materialista.ClaseId);
            
            return alumno != null;
        }
    }
}