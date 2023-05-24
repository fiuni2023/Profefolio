using Microsoft.EntityFrameworkCore;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services;

public class DocumentoService : IDocumento
{
    private ApplicationDbContext _dbContext;
    private bool _disposed;

    public DocumentoService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Documento> FindById(int id)
    {
        return await _dbContext.Documentos
            .Where(p => !p.Deleted && p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Documento> FindByNameDocumento(string n)
    {
        return await _dbContext.Documentos
            .Where(p => !p.Deleted && p.Nombre == n)
            .FirstOrDefaultAsync();
    }
   
    public Documento Edit(Documento t)
    {
        _dbContext.Entry(t).State = EntityState.Modified;
        return t;
    }

    public async Task<Documento> Add(Documento t)
    {
        var result = await _dbContext.Documentos.AddAsync(t);
        return result.Entity;
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public int Count()
    {
        return _dbContext.Documentos
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

    public IEnumerable<Documento> GetAll(int page, int cantPorPag)
    {
        return _dbContext.Documentos
         .Where(p => !p.Deleted)
         .Skip(page * cantPorPag)
         .Take(cantPorPag);
    }

   
}