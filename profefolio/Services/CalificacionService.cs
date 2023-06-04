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


    public PlanillaDTO GetAll(int idMateriaLista, string user)
    {
        var alumnosQuery = _db.ClasesAlumnosColegios
            .Include(c => c.Evaluaciones)
            .ThenInclude(x => x.Evaluacion)
            .ThenInclude(x => x.MateriaList)
            .Where(c => !c.Deleted)
            .Where(c => c.Evaluaciones
                .Any(y => y.Evaluacion.MateriaListaId == idMateriaLista));

        var materia = _db.MateriaListas
            .Include(m => m.Materia)
            .Where(m => !m.Deleted && m.Id == idMateriaLista)
            .Select(m => m.Materia.Nombre_Materia)
            .First();
            
        var planilla = new PlanillaDTO
        {
            MateriaId = idMateriaLista,
            Materia = materia,
            Etapas = new List<EtapaDTO>()
        };

        foreach (var alumno in alumnosQuery)
        {
            var evaluacionesAlumnos = alumno.Evaluaciones;

            if (evaluacionesAlumnos.IsNullOrEmpty())
            {
                
            }
            foreach (var evaluacionAlumno in evaluacionesAlumnos)
            {
                
            }
        }


        return planilla;
    }

    private void CargarEvaluaciones(ClasesAlumnosColegio cac, string user, int idEvaluacion)
    {
        var cantEvaluaciones = _db.ClasesAlumnosColegios
            .Include(c => c.Evaluaciones)
            .Where(c => !c.Deleted && cac.Id != c.Id)
            .Where(c => c.Evaluaciones.Any())
            .Select(c => c.Evaluaciones)
            .First()
            .Count;


        for (var i = 0; i < cantEvaluaciones; i++)
        {
            cac.Evaluaciones.Add(new EvaluacionAlumno()
            {
                ClasesAlumnosColegioId = cac.Id,
                Created = DateTime.Now,
                CreatedBy = user,
                EvaluacionId = idEvaluacion,
                PuntajeLogrado = 0,
                PorcentajeLogrado = 0
            });
        }
    }

   
}