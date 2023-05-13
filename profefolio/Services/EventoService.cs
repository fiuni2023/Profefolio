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
    public async Task<Evento> FindById(int id)
    {
        return await _dbContext.Eventos
            .Where(p => !p.Deleted && p.Id == id)
            .FirstOrDefaultAsync();
    }

    /*public async Task<Evento> FindByNameMateria(string n)
    {
        return await _dbContext.Materias
            .Where(p => !p.Deleted && p.Nombre_Materia == n)
            .FirstOrDefaultAsync();
    }
    public async Task<Materia> FindByNameMateriaId(string n, int id)
    {
        return await _dbContext.Materias
            .Where(p => !p.Deleted && p.Nombre_Materia == n && p.Id != id)
            .FirstOrDefaultAsync();
    }*/

    public async Task<List<Evento>> GetAll()
    {
        return await _dbContext.Eventos.Where(p => !p.Deleted).ToListAsync();
    }

    public Materia Edit(Evento t)
    {
        _dbContext.Entry(t).State = EntityState.Modified;
        return t;
    }

    public async Task<Evento> Add(Evento t)
    {
        var result = await _dbContext.Evento.AddAsync(t);
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

    public IEnumerable<Evento> GetAll(int page, int cantPorPag)
    {
        return _dbContext.Eventos
         .Where(p => !p.Deleted)
         .Skip(page * cantPorPag)
         .Take(cantPorPag);
    }

    
}