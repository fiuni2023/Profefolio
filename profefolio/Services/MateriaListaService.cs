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

        public IEnumerable<MateriaLista> FilterByIdMateriaAndUserAndClass(int idMateria, string createdBy, int idClase)
        {
            return _db.MateriaListas.Where(d => !d.Deleted)
                .Where(d => d.Id == idMateria)
                .Where(d => d.CreatedBy.Equals(createdBy))
                .Where(d => d.ClaseId == idClase);
        }

        public async Task<IEnumerable<MateriaLista>> GetDetalleClaseByIdMateriaAndUsername(string username, int idMateria)
        {
            var query = await _db.Materias
                .Include(m => m.MateriaListas)
                .Where(m => !m.Deleted)
                .Where(m => m.CreatedBy.Equals(username))
                .FirstOrDefaultAsync();

            if (query == null) throw new FileNotFoundException();

            return query.MateriaListas
                .Where(l => !l.Deleted)
                .Where(l => l.CreatedBy.Equals(username));

        }
    }
}
