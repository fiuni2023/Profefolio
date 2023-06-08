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


    public async Task<PlanillaDTO> GetAll(int idMateriaLista, string user)
    {
        var materiaListaQuery = _db.MateriaListas
            .Include(p => p.Profesor)
            .Include(m => m.Materia);
        var materiaListaVerify = materiaListaQuery 
            .Where(ml => !ml.Deleted)
            .Any(ml => ml.Profesor.Email.Equals(user));


        if (!materiaListaVerify)
        {
            throw new UnauthorizedAccessException();
        }
        var evaluacionQuery = _db.Eventos
            .Include(e => e.MateriaList)
            .ThenInclude(m =>m==null?null: m.Materia)
            .Include(ea => ea.EvaluacionAlumnos)
            .ThenInclude(a => a.ClasesAlumnosColegio)
            .ThenInclude(ca => ca == null ? null : ca.ColegiosAlumnos)
            .ThenInclude(p =>p == null ? null : p.Persona)
            .Where(x => !x.Deleted && x.MateriaListaId == idMateriaLista)
            .GroupBy(x => x.Etapa)
            .Select(x => new
            {
                Etapa = x.Key,
                Evaluaciones = x.GetEnumerator()
            });

        var planilla = new PlanillaDTO
        {
            Materia = await materiaListaQuery
                .Select(ml => ml.Materia.Nombre_Materia)
                .FirstAsync(),
            Etapas = new List<EtapaDTO>(),
            MateriaId = idMateriaLista
        };

        foreach (var etapa in evaluacionQuery)
        {
            var etapaDto = new EtapaDTO
            {
                Etapa = etapa.Etapa,
                Alumnos = new List<AlumnoWithPuntajesDTO>()
            };
            
            
            while (etapa.Evaluaciones.MoveNext())
            {
                var evaluacion = etapa.Evaluaciones.Current;
                foreach (var alumnoEvaluacion in evaluacion.EvaluacionAlumnos)
                {
                    var alumnoPuntaje = new AlumnoWithPuntajesDTO
                    {
                        AlumnoId = alumnoEvaluacion.ClasesAlumnosColegioId,
                        Nombre = alumnoEvaluacion.ClasesAlumnosColegio?.ColegiosAlumnos.Persona.Nombre,
                        Apellido = alumnoEvaluacion.ClasesAlumnosColegio?.ColegiosAlumnos.Persona.Apellido,
                        Doc = alumnoEvaluacion.ClasesAlumnosColegio?.ColegiosAlumnos.Persona.Documento,
                        PuntajeTotalLogrado = evaluacion.EvaluacionAlumnos.Sum(ev => ev.PuntajeLogrado),
                        PorcentajeTotalLogrado = evaluacion.EvaluacionAlumnos.Sum(ev => ev.PuntajeLogrado),
                        EvaluacionId = alumnoEvaluacion.EvaluacionId,
                        Puntajes = evaluacion.EvaluacionAlumnos.Select(ev => new PuntajeDTO
                        {
                            PuntajeLogrado = ev.PuntajeLogrado,
                            PorcentajeLogrado = ev.PorcentajeLogrado,
                            PuntajeTotal = evaluacion.PuntajeTotal
                        }).ToList()
                    };
                    etapaDto.Alumnos.Add(alumnoPuntaje);
                }

            }
            planilla.Etapas.Add(etapaDto);
        }


        return planilla;
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

    public Task<PlanillaDTO> Put(int idMAteriaLista, CalificacionPutDto dto, string user)
    {
        
        throw new NotImplementedException();
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