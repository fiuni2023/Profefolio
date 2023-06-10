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


    //TO-DO Corregir
    public Task<PlanillaDTO> GetAll(int idMateriaLista, string user)
    {
        throw new NotImplementedException();
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

    public async Task<PlanillaDTO> Put(int idMateriaLista, CalificacionPutDto dto, string user)
    {
        var materiaListaQuery = _db.MateriaListas
            .Include(p => p.Profesor)
            .Include(m => m.Materia);
        
        var materiaListaVerify = materiaListaQuery 
            .Where(ml => !ml.Deleted)
            .Any(ml => ml.Profesor.Email.Equals(user) && ml.Id == idMateriaLista);


        if (!materiaListaVerify)
        {
            throw new UnauthorizedAccessException();
        }

        var ev = await _db.EventoAlumnos
            .Include(e => e.Evaluacion)
            .Where(ev => !ev.Deleted && ev.Id == dto.IdEvaluacion)
            .FirstOrDefaultAsync();

        if (ev == null)
        {
            throw new FileNotFoundException();
        }

        ev.PuntajeLogrado = dto.Puntaje;
        ev.PorcentajeLogrado = (dto.Puntaje * 100) / ev.Evaluacion.PuntajeTotal;
        
        await _db.SaveChangesAsync();
        await this.Verify(idMateriaLista, user);
        var result = await this.GetAll(idMateriaLista, user);
        return result;
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