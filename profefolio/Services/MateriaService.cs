using Microsoft.EntityFrameworkCore;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services;

public class MateriaService : IMateria
{
    private ApplicationDbContext _dbContext;
    private bool _disposed;

    public MateriaService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Materia> FindById(int id)
    {
        return await _dbContext.Materias
            .Where(p => !p.Deleted && p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Materia> FindByNameMateria(string n)
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
    }
    public async Task<List<Materia>> GetAll()
    {
        return await _dbContext.Materias.Where(p => !p.Deleted).ToListAsync();
    }

    public Materia Edit(Materia t)
    {
        _dbContext.Entry(t).State = EntityState.Modified;
        return t;
    }

    public async Task<Materia> Add(Materia t)
    {
        var result = await _dbContext.Materias.AddAsync(t);
        return result.Entity;
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public int Count()
    {
        return _dbContext.Materias
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

    public IEnumerable<Materia> GetAll(int page, int cantPorPag)
    {
        return _dbContext.Materias
         .Where(p => !p.Deleted)
         .Skip(page * cantPorPag)
         .Take(cantPorPag);
    }

    public async Task<List<Materia>> FindAllUnsignedMaterias(int idClase)
    {
        return await _dbContext.Materias
                .Where(m => !m.Deleted
                    && m.MateriaListas.Where(ml => ml.ClaseId != idClase)
                    .Count() == 0)
                .ToListAsync();
    }
}