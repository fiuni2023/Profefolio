using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;
using Microsoft.EntityFrameworkCore;

namespace profefolio.Services
{
    public class AnotacionesService : IAnotacion
    {
        private ApplicationDbContext _context;

        public AnotacionesService(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<Anotacion> Add(Anotacion t)
        {
            var result = await _context.Anotaciones.AddAsync(t);
            return result.Entity;
        }

        public int Count()
        {
            return _context.Anotaciones.Where(c => !c.Deleted).Count();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Anotacion Edit(Anotacion t)
        {
            _context.Entry(t).State = EntityState.Modified;
            return t;
        }

        public bool Exist()
        {
            return _context.Anotaciones.Where(c => !c.Deleted).Any();
        }

        public async Task<Anotacion> FindById(int id)
        {
            return await _context.Anotaciones.Where(c => !c.Deleted && c.Id == id).FirstOrDefaultAsync();
        }

        public IEnumerable<Anotacion> GetAll(int page, int cantPorPag)
        {
            return _context.Anotaciones.Where(c => !c.Deleted).Skip(page * cantPorPag).Take(cantPorPag).ToList();
        }

        public async Task<IEnumerable<Anotacion>> GetAll()
        {
            return await _context.Anotaciones.Where(c => !c.Deleted).ToArrayAsync();
        }

        public async Task<IEnumerable<Anotacion>> GetAll(int idMateriaLista, string emailProfesor)
        {
            return await _context.Anotaciones
                        .Include(a => a.MateriaLista)
                        .Include(a => a.MateriaLista.Profesor)
                        .Where(a => !a.Deleted 
                        && a.MateriaListaId == idMateriaLista 
                        && !a.MateriaLista.Deleted
                        && !a.MateriaLista.Profesor.Deleted
                        && emailProfesor.Equals(a.MateriaLista.Profesor.Email))
                        .ToListAsync();
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<bool> VerificacionPreguardado(int idMateriaLista, string emailProfesor, string tituloNuevo)
        {       
            // se verificca que el prof ensenhe en la materia
            var verifMateria =  await _context.Anotaciones
                        .Include(a => a.MateriaLista)
                        .Include(a => a.MateriaLista.Profesor)
                        .FirstOrDefaultAsync(a => !a.Deleted
                        && a.MateriaListaId == idMateriaLista 
                        && !a.MateriaLista.Deleted
                        && !a.MateriaLista.Profesor.Deleted
                        && emailProfesor.Equals(a.MateriaLista.Profesor.Email));
            
            if(verifMateria == null){
                throw new UnauthorizedAccessException();
            }

            var verifNombreRepetido = await _context.Anotaciones
                        .Include(a => a.MateriaLista)
                        .FirstOrDefaultAsync(a => !a.Deleted
                            && a.Titulo.ToLower().Equals(tituloNuevo.ToLower())
                            && a.MateriaListaId == idMateriaLista
                            && !a.MateriaLista.Deleted); 
            
            return verifMateria != null && verifNombreRepetido == null;
        }

        public async Task<bool> VerificarProfesor(int idAnotacion, string emailProfesor)
        {
            return await _context.Anotaciones
                    .Include(a => a.MateriaLista)
                    .Include(a => a.MateriaLista.Profesor)
                    .AnyAsync(a => !a.Deleted 
                        && a.Id == idAnotacion 
                        && !a.MateriaLista.Deleted
                        && !a.MateriaLista.Profesor.Deleted
                        && emailProfesor.Equals(a.MateriaLista.Profesor.Email)); 
        }
    }
}