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
    public async Task<PlanillaDTO> GetAll(int idMateriaLista, string user)
    {

        var alumnoQuery = _db.ClasesAlumnosColegios
            .Include(ev => ev.Evaluaciones
                .Where(x => !x.Deleted))
            .ThenInclude(e => e.Evaluacion)
            .Include(ca => ca.ColegiosAlumnos)
            .ThenInclude(a => a.Persona)
            .Where(aq => !aq.Deleted && aq.Evaluaciones
                .Any(x => x.Evaluacion != null && x.Evaluacion.MateriaListaId == idMateriaLista && !x.Deleted));
            
        var materiaQuery = await _db.MateriaListas
            .Include(x => x.Materia)
            .Where(x => x.Id == idMateriaLista && !x.Deleted)
            .Select(x => x.Materia.Nombre_Materia)
            .FirstOrDefaultAsync();
        
        if (materiaQuery == null)
            throw new FileNotFoundException();

        var planilla = new PlanillaDTO
        {
            MateriaId = idMateriaLista,
            Materia = materiaQuery,
            Alumnos = new List<AlumnoWithPuntajesDTO>()
        };

        foreach (var al in alumnoQuery)
        {
            var alumnoPuntaje = new AlumnoWithPuntajesDTO
            {
                Nombre = al.ColegiosAlumnos.Persona?.Nombre,
                AlumnoId = al.Id,
                Apellido = al.ColegiosAlumnos.Persona?.Apellido,
                Doc = al.ColegiosAlumnos.Persona?.Documento,
                Etapas = new List<EtapaDTO>()
            };

            var evaluaciones = al.Evaluaciones
                .GroupBy(x => x.Evaluacion?.Etapa)
                .Select(x => new
                {
                    Etapa = x.Key,
                    Evaluaciones = x.AsEnumerable()
                });
            
            

            foreach (var ev in evaluaciones)
            {   
                double puntajeTotalLogrado = ev.Evaluaciones
                    .Where(e => !e.Deleted)
                    .Where(e => e.Evaluacion != null && e.Evaluacion.MateriaListaId == idMateriaLista)
                    .Where(e => e.ClasesAlumnosColegioId == al.Id)
                    .Sum(e => e.PuntajeLogrado);

                var total = ev.Evaluaciones
                    .Where(x => !x.Deleted)
                    .Select(x => x.Evaluacion?.PuntajeTotal)
                    .Sum();

                if (total is null or 0)
                {
                    total = 1;
                }

                var porcentajeTotal = (puntajeTotalLogrado / total) * 100;

                var etapa = new EtapaDTO
                {
                    Etapa = ev.Etapa,
                    Puntajes = ev.Evaluaciones
                        .Where(e => !e.Deleted)
                        .Where(e => e.Evaluacion != null && e.Evaluacion.MateriaListaId == idMateriaLista)
                        .Where(e => e.ClasesAlumnosColegioId == al.Id)
                        .Select(e =>
                        {
                            if (e.Evaluacion != null)
                                return new PuntajeDTO()
                                {
                                    PuntajeLogrado = e.PuntajeLogrado,
                                    PorcentajeLogrado = e.PorcentajeLogrado,
                                    PuntajeTotal = e.Evaluacion.PuntajeTotal,
                                    IdEvaluacion = e.Id,
                                    NombreEvaluacion = e.Evaluacion.Nombre,
                                    Tipo = e.Evaluacion.Tipo

                                };
                            return null;
                        } ).ToList(),
                    PuntajeTotalLogrado = puntajeTotalLogrado,
                    PorcentajeTotalLogrado = (double) porcentajeTotal
                };

                
                
                alumnoPuntaje.Etapas.Add(etapa);
            }
            
            planilla.Alumnos.Add(alumnoPuntaje);
        }

        return planilla;
    }

    public async Task<bool> Verify(int idMateriaLista, string user)
    {
        var alumnosQuery = _db.ClasesAlumnosColegios
            .Include(c => c.Evaluaciones)
            .ThenInclude(x => x.Evaluacion)
            .Where(c => !c.Deleted)
            .ToList();



        foreach (var alumno in alumnosQuery)
        {
            var evaluacionesAlumnos = _db.EventoAlumnos
                .Include(x => x.Evaluacion)
                .Where(x => x.Evaluacion != null
                            && !x.Deleted 
                            && x.ClasesAlumnosColegioId == alumno.Id 
                            && x.Evaluacion.MateriaListaId == idMateriaLista)
                .ToList();

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
            .Where(ev => ev.Id == dto.IdEvaluacion)
            .FirstOrDefaultAsync();

        if (ev == null)
        {
            throw new FileNotFoundException();
        }

        switch (dto.Modo)
        {
            case "p" :
                ev.PuntajeLogrado = dto.Puntaje;
                if(ev.Evaluacion != null && ev.Evaluacion.PuntajeTotal < ev.PuntajeLogrado)
                {
                    ev.Evaluacion.PuntajeTotal = ev.PuntajeLogrado;
                }
                if (ev.Evaluacion != null) ev.PorcentajeLogrado = (dto.Puntaje * 100) / ev.Evaluacion.PuntajeTotal;
                await _db.SaveChangesAsync();
        
                await this.Verify(idMateriaLista, user);
                var result = await this.GetAll(idMateriaLista, user);
                return result;
            case "pt" :
                if (ev.Evaluacion != null)
                {
                    ev.Evaluacion.PuntajeTotal = dto.PuntajeTotal;
                    if (ev.Evaluacion.PuntajeTotal < ev.PuntajeLogrado)
                        ev.PuntajeLogrado = ev.Evaluacion.PuntajeTotal;
                    
                    ev.PorcentajeLogrado = (ev.PuntajeLogrado * 100) / ev.Evaluacion.PuntajeTotal;
                    await _db.SaveChangesAsync();
                }

                await this.Verify(idMateriaLista, user);
                return await this.GetAll(idMateriaLista, user);
            case "nn":
                if (ev.Evaluacion != null)
                {
                    ev.Evaluacion.Nombre = dto.NombreEvaluacion;
                    await _db.SaveChangesAsync();
                }
                await this.Verify(idMateriaLista, user);
                return await this.GetAll(idMateriaLista, user);
            case  "d" :
                if (ev.Evaluacion != null) ev.Evaluacion.Deleted = true;

                var qea = _db.EventoAlumnos
                    .Where(d => !d.Deleted && d.EvaluacionId == ev.EvaluacionId);
                foreach (var q in qea)
                {
                    q.Deleted = true;
                    q.Modified = DateTime.Now;
                    q.ModifiedBy = user;
                }
                await _db.SaveChangesAsync();
                await this.Verify(idMateriaLista, user);
                return await this.GetAll(idMateriaLista, user);
                
            default:
                throw new BadHttpRequestException("Modo no valido");
        }

       
    }

    private async Task CargarEvaluaciones(ClasesAlumnosColegio cac, string user)
    {
        var evaluacionesQ = _db.ClasesAlumnosColegios
            .Include(c => c.Evaluaciones)
            .Where(c => !c.Deleted && cac.Id != c.Id)
            .Where(c => c.Evaluaciones.Any())
            .Select(c => c.Evaluaciones)
            .FirstOrDefault();



        if (evaluacionesQ != null)
        {
            var evaluaciones = evaluacionesQ.Select(w => w.EvaluacionId)
                .ToList();


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

   
}