using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using profefolio.Models;
using profefolio.Models.DTOs.Calificacion;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services;

public class CalificacionService : ICalificacion
{
    private ApplicationDbContext _db;
    private bool _disposedValue; 

    public CalificacionService(ApplicationDbContext db)
    {
        _db = db;
        _disposedValue = false;
    }
    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue) return;
        if (disposing)
        {
            _db.Dispose();
        }
        _disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }


    public PlanillaDTO GetAll(int idMateriaLista)
    {
        var evaluacionesQuery = _db.ClasesAlumnosColegios
            .Where(c => !c.Deleted);
            
        return null;
    }

   
}