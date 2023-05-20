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
            _context.Entry(t).State = EntityState.Added;
            return await Task.FromResult(t);
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public async Task<Asistencia> Delete(Asistencia t)
        {
            _context.Entry(t).State = EntityState.Deleted;
            return await Task.FromResult(t);
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }

        public Asistencia Edit(Asistencia t)
        {
            _context.Entry(t).State = EntityState.Modified;
            return t;
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistAsistenciaInDate(int idMateriaLista, int idClaseColegioAlumno, DateTime fecha)
        {
            return await _context.Asistencias
                    .AnyAsync(a => !a.Deleted 
                        && a.MateriaListaId == idMateriaLista
                        && a.ClasesAlumnosColegioId == idClaseColegioAlumno
                        && a.Fecha.Date == fecha.Date);
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

        public async Task<Asistencia> FindByIdAndProfesorId(int id, string profesorId)
        {
            var asistencia = await _context.Asistencias
                .FirstOrDefaultAsync(a => !a.Deleted 
                    && a.Id == id
                    && profesorId.Equals(a.MateriaLista.ProfesorId));
            
            if(asistencia == null){
                throw new FileNotFoundException("No se encontro la asistencia");
            }
            return asistencia;
        }

        public IEnumerable<Asistencia> GetAll(int page, int cantPorPag)
        {
            throw new NotImplementedException();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}