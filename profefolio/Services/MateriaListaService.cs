using System.Linq;
using Microsoft.EntityFrameworkCore;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services
{
    public class MateriaListaService : IMateriaLista
    {
        private bool disposedValue;
        private readonly ApplicationDbContext _db;

        public MateriaListaService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<MateriaLista> Add(MateriaLista t)
        {
            var saved = await _db.MateriaListas.AddAsync(t);

            return saved.Entity;
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public MateriaLista Edit(MateriaLista t)
        {
            _db.Entry(t).State = EntityState.Modified;
            return t;
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public async Task<MateriaLista> FindById(int id)
        {
            var query = await _db.MateriaListas
                .FirstOrDefaultAsync(p => p.Id == id && !p.Deleted);

            if (query == null) throw new FileNotFoundException();

            return query;
        }

        public IEnumerable<MateriaLista> GetAll(int page, int cantPorPag)
        {
            return _db.MateriaListas
                .Include(p => p.Clase)
                .Include(p => p.Profesor)
                .Include(p => p.Materia);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }



        public async Task<bool> IsUsedMateria(int idMateria)
        {
            return await _db.MateriaListas.AnyAsync(d => d.MateriaId == idMateria);
        }

        public async Task<MateriaLista> Find(int idClase, string idProfesor, int idMateria, string userLogged)
        {
            var colegio = _db.Colegios
                .FirstOrDefault(c => c.personas.UserName.Equals(userLogged));


            if (colegio == null) throw new FileNotFoundException();

            var clase = await _db.Clases
                .Include(c => c.MateriaListas)
                .Where(c => !c.Deleted)
                .Where(c => c.ColegioId == colegio.Id)
                .Where(c => c.Id == idClase)
                .FirstOrDefaultAsync();

            if (clase == null) throw new FileNotFoundException();

            var materiaListas = clase.MateriaListas
                .Where(p => p.ClaseId == idClase)
                .Where(p => p.MateriaId == idMateria)
                .Where(p => p.ProfesorId == idProfesor)
                .FirstOrDefault();

            return materiaListas;


        }

        public async Task<IEnumerable<MateriaLista>> FindByIdClase(int idClase, string user)
        {
            var colegio = await _db.Colegios
                .Include(c => c.personas)
                .Where(c => !c.Deleted)
                .Where(c => c.personas.Email.Equals(user))
                .FirstOrDefaultAsync();

            if (colegio == null)
            {
                throw new BadHttpRequestException("Accion no valida");
            }
            var clase = await _db.Clases
                .Include(c => c.MateriaListas)
                .Where(c => !c.Deleted)
                .Where(c => c.Id == idClase)
                .FirstOrDefaultAsync();

            if (clase == null || (colegio.Id != clase.ColegioId) || clase.MateriaListas == null)
            {
                throw new FileNotFoundException();
            }

            return clase.MateriaListas;
        }

        public Task<MateriaLista> DeleteByIdClase(int idClase, string user)
        {
            throw new NotImplementedException();
        }
    }
}
