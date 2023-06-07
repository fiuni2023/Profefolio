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


    public async Task<bool> Verify(int idMateriaLista, string user)
    {
        var alumnosQuery = _db.ClasesAlumnosColegios
            .Include(c => c.Evaluaciones)
            .ThenInclude(x => x.Evaluacion)
            .ThenInclude(x => x.MateriaList)
            .Where(c => !c.Deleted)
            .Where(c => c.Evaluaciones
                .Any(y => y.Evaluacion.MateriaListaId == idMateriaLista));



        foreach (var alumno in alumnosQuery)
        {
            var evaluacionesAlumnos = alumno.Evaluaciones;

            if (evaluacionesAlumnos.IsNullOrEmpty())
            {
                await CargarEvaluaciones(alumno, user);
            }
        }


        return true;
    }

    private async Task CargarEvaluaciones(ClasesAlumnosColegio cac, string user)
    {
        var evaluaciones = _db.ClasesAlumnosColegios
            .Include(c => c.Evaluaciones)
            .Where(c => !c.Deleted && cac.Id != c.Id)
            .Where(c => c.Evaluaciones.Any())
            .Select(c => c.Evaluaciones)
            .First()
            .Select(w => w.EvaluacionId);


        foreach (var e in evaluaciones)
        {
            cac.Evaluaciones.Add(new EvaluacionAlumno()
            {
                ClasesAlumnosColegioId = cac.Id,
                Created = DateTime.Now,
                CreatedBy = user,
                EvaluacionId = e,
                PuntajeLogrado = 0,
                PorcentajeLogrado = 0
            });
        }

        await _db.SaveChangesAsync();
    }

   
}