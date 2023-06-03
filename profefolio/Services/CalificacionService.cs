using profefolio.Models;
using profefolio.Models.DTOs.Calificacion;
using profefolio.Repository;

namespace profefolio.Services;

public class CalificacionService : ICalificacion
{
    private ApplicationDbContext _db;

    public CalificacionService(ApplicationDbContext db)
    {
        _db = db;
    }
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public PlanillaDTO GetAll(int idMateriaLista)
    {
        throw new NotImplementedException();
    }
}