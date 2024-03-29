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

        public async Task<bool> Exist(int idMateriaLista, int idHoraCatedra, string dia)
        {
            return await _context.HorasCatedrasMaterias.AnyAsync(a =>
                    !a.Deleted
                    && a.MateriaListaId == idMateriaLista
                    && a.HoraCatedraId == idHoraCatedra
                    && a.Dia != null && dia.ToLower().Equals(a.Dia.ToLower()));
        }

        public Task<HorasCatedrasMaterias> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HorasCatedrasMaterias> GetAll(int page, int cantPorPag)
        {
            throw new NotImplementedException();
        }

        public async Task<List<HorasCatedrasMaterias>> GetAll()
        {
            return await _context.HorasCatedrasMaterias
                            .Where(a => !a.Deleted)
                            .Include(a => a.HoraCatedra)
                            .Include(a => a.MateriaLista)
                            .ToListAsync();
        }

        public async Task<List<HorasCatedrasMaterias>> GetAllHorariosByEmailProfesorAndYear(string emailProfesor, int anho)
        {
            return await _context.HorasCatedrasMaterias
            .Include(a => a.HoraCatedra)
            .Include(a => a.MateriaLista)
            .Include(a => a.MateriaLista.Materia)
            .Include(a => a.MateriaLista.Profesor)
            .Include(a => a.MateriaLista.Clase.Colegio)
            .Where(a => !a.Deleted 
                && emailProfesor.Equals(a.MateriaLista.Profesor.Email) && a.MateriaLista.Clase.Anho == anho)/* .GroupBy(a => a.MateriaLista.Clase.Colegio) */
            .OrderBy(a => a.HoraCatedra.Inicio)
            .ToListAsync();

        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}