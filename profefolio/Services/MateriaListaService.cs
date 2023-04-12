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

        public MateriaListaService(bool disposedValue, ApplicationDbContext db)
        {
            this.disposedValue = disposedValue;
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
    }
}
