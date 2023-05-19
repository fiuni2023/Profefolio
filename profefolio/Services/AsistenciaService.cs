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

        public async Task<List<IGrouping<int, Asistencia>>> FindAll(int idMateriaLista, string userEmail)
        {
            var limit = DateTime.Now;
            limit.AddDays(limit.Day - 5);
            return await _context.Asistencias
                        .OrderBy(a => a.Fecha)
                        .TakeWhile(a => !a.Deleted 
                                    && a.Fecha > limit 
                                    && a.MateriaListaId == idMateriaLista
                                    && !a.MateriaLista.Deleted
                                    && !a.MateriaLista.Profesor.Deleted
                                    && userEmail.Equals(a.MateriaLista.Profesor.Email))
                                    .GroupBy(a => a.ClasesAlumnosColegioId).ToListAsync();
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