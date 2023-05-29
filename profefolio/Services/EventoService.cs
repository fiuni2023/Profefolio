using Microsoft.EntityFrameworkCore;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services;

public class EventoService : IEvento
{
    private ApplicationDbContext _dbContext;
    private bool _disposed;

    public EventoService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Evaluacion> FindById(int id)
    {
        return await _dbContext.Eventos
            .Where(p => !p.Deleted && p.Id == id)
            .FirstOrDefaultAsync();
    }
    public async Task<Evaluacion> FindById2(int id)
    {
        /*
        return await _dbContext.Eventos
            .Where(p => !p.Deleted && p.Id == id)
            .Include(e => e.Materias)
            .Include(e => e.Clases)
            .Include(e => e.Colegios)
            .FirstOrDefaultAsync();
            
        */
        throw new NotImplementedException();
    }


    public async Task<Evaluacion> FindByEventoRepetido(String t, DateTime f, int c, int m, int col)
    {
        /*
        return await _dbContext.Eventos
            .Where(p => !p.Deleted && p.Tipo == t && p.Fecha == f
             && p.MateriaId == m && p.ClaseId == c && p.ColegioId == col)
            .FirstOrDefaultAsync();
        */
        throw new NotImplementedException();
    }

    public async Task<List<Evaluacion>> GetAll(String prfId)
    {
        /*
        return await _dbContext.Eventos
        .Where(p => !p.Deleted)
        .Where(p => p.ProfesorId == prfId)
        .Include(e => e.Materias)
        .Include(e => e.Clases)
        .Include(e => e.Colegios)
        .ToListAsync();
        */
        throw new NotImplementedException();
    }

    public Evaluacion Edit(Evaluacion t)
    {
        _dbContext.Entry(t).State = EntityState.Modified;
        return t;
    }

    public async Task<Evaluacion> Add(Evaluacion t)
    {
        var result = await _dbContext.Eventos.AddAsync(t);
        return result.Entity;
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public int Count()
    {
        return _dbContext.Eventos
            .Count(p => !p.Deleted);
    }

    public bool Exist()
    {
        return Count() > 0;
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public IEnumerable<Evaluacion> GetAll(int page, int cantPorPag)
    {
        return _dbContext.Eventos
         .Where(p => !p.Deleted)
         .Skip(page * cantPorPag)
         .Take(cantPorPag);
    }

}