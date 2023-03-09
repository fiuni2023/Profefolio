using Microsoft.EntityFrameworkCore;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services;

public class ColegiosFullService : IFullColegio
{
    private ApplicationDbContext _dbContext;
    private bool _disposed;

    public ColegiosFullService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Colegio> FindById(int id)
    {
        return await _dbContext.Colegios
            .Where(p => !p.Deleted  && p.Id == id).Include(b => b.personas)
            .FirstOrDefaultAsync();
    }

    public IEnumerable<Colegio> GetAll()
    {
        return _dbContext.Colegios.Where(p => p.Deleted);
    }

    public Colegio Edit(Colegio t)
    {
        _dbContext.Entry(t).State = EntityState.Modified;
        return t;
    }

    public async Task<Colegio> Add(Colegio t)
    {
        var result = await _dbContext.Colegios.AddAsync(t);
        return result.Entity;
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public int Count()
    {
        return _dbContext.Colegios
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

     public IEnumerable<Colegio> GetAll(int page, int cantPorPag)
    {
           return _dbContext.Colegios
            .Where(p => !p.Deleted).Include(b => b.personas)
            .Skip(page*cantPorPag)
            .Take(cantPorPag);
    }
}