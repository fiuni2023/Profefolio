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
    public class AsistenciaService : IAsistencia
    {
        private readonly ApplicationDbContext _context;
        public AsistenciaService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Asistencia> Add(Asistencia t)
        {
            var result = await _context.Asistencias.AddAsync(t);
            return result.Entity;
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }

        public Asistencia Edit(Asistencia t)
        {
            throw new NotImplementedException();
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ClasesAlumnosColegio>> FindAll(int idMateriaLista, string userEmail)
        {

            var materiaLista = await _context.MateriaListas
                .Where(a => !a.Deleted 
                            && a.Id == idMateriaLista 
                            && userEmail.Equals(a.Profesor.Email))
                .FirstOrDefaultAsync();
            
            if(materiaLista == null){
                throw new FileNotFoundException("No se encontraron materias asignadas");
            }
            
            return await _context.ClasesAlumnosColegios
                .Where(a => !a.Deleted 
                    && a.Clase.MateriaListas.Any(m => m.Id == idMateriaLista))
                .Include(a => a.Asistencias)
                .Include(a => a.ColegiosAlumnos)
                .Include(a => a.ColegiosAlumnos.Persona)
                .OrderBy(a => a.ColegiosAlumnos.Persona.Apellido)
                .ToListAsync();
        }
        
        public Task<Asistencia> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Asistencia> GetAll(int page, int cantPorPag)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }
    }
}