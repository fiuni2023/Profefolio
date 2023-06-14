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
    
    public async Task<Evaluacion> Add(Evaluacion t, string user)
    {
        var profesor =await _dbContext.Users
            .Where(p => !p.Deleted)
            .FirstOrDefaultAsync(p => p.Email.Equals(user));

        var verifyProfe = await _dbContext.MateriaListas
            .AnyAsync(ml => !ml.Deleted && ml.Id == t.MateriaListaId);
        
        
            
        
        
        if (!verifyProfe || profesor == null)
        {
            throw new UnauthorizedAccessException();
        }

        var claseId = await _dbContext.MateriaListas
            .Where(ml => !ml.Deleted)
            .Where(ml => ml.ProfesorId.Equals(profesor.Id) && ml.Id == t.MateriaListaId)
            .Select(c => c.ClaseId)
            .FirstOrDefaultAsync();


        var alumnosQuery = _dbContext.ClasesAlumnosColegios
            .Where(cac => !cac.Deleted)
            .Where(cac => cac.ClaseId == claseId);

        t.EvaluacionAlumnos = new List<EvaluacionAlumno>();

        foreach (var alumno in alumnosQuery)
        {
            var evaluacion = new EvaluacionAlumno
            {
                PuntajeLogrado = 0,
                PorcentajeLogrado = 0,
                ClasesAlumnosColegioId = alumno.Id,
                Created = DateTime.Now,
                CreatedBy = user
            };
            
            t.EvaluacionAlumnos.Add(evaluacion);
        }
        
        var result = await _dbContext.Eventos.AddAsync(t);
        return result.Entity;
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
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