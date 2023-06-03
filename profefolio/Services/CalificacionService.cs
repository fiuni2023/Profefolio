using Microsoft.EntityFrameworkCore;
using profefolio.Models;
using profefolio.Models.DTOs.Calificacion;
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
        var evaluacionesQuery = _db.Eventos
                .Include(c => c.EvaluacionAlumnos)
                .Include(c => c.MateriaList)
                .Where(c => c.Deleted);

        
        var result = new PlanillaDTO
        {
            MateriaId = idMateriaLista,
            Etapas = new List<EtapaDTO>()
        };

        foreach (var evaluacion in evaluacionesQuery)
        {
            var etapa = new EtapaDTO
            {
                Etapa = evaluacion.Etapa,
                Alumnos = new List<AlumnoWithPuntajesDTO>()
            };
            
            result.Etapas.Add(etapa);
        }

        throw new NotImplementedException();
    }

    //TODO Validar Si algun alumno se integra luego de empezar las clases
    private void ValidarPlanilla(int idMateriaLista)
    {
        
    }
}