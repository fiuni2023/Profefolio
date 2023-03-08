using Microsoft.EntityFrameworkCore;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services;

public class PersonasService : IPersona
{
    private ApplicationDbContext _dbContext;
    private bool _disposed;

    public PersonasService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Persona> FindById(int id)
    {
        Console.Write("en find persona...");
        return await _dbContext.Personas
            .Where(p => !p.Deleted && p.Id == id)
            .FirstOrDefaultAsync();
    }

    public IEnumerable<Persona> GetAll()
    {
        return _dbContext.Personas.Where(p => !p.Deleted);
    }

    public Persona Edit(Persona t)
    {
        _dbContext.Entry(t).State = EntityState.Modified;
        return t;
    }

    public async Task<Persona> Add(Persona t)
    {
        var result = await _dbContext.Personas.AddAsync(t);
        return result.Entity;
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public int Count()
    {
        return _dbContext.Personas
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
}