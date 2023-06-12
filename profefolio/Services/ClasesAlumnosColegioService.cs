using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;
using Microsoft.EntityFrameworkCore;
using profefolio.Models.DTOs.ClasesAlumnosColegio;

namespace profefolio.Services
{
    public class ClasesAlumnosColegioService : IClasesAlumnosColegio
    {
        private ApplicationDbContext _context;
        public ClasesAlumnosColegioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ClasesAlumnosColegio> Add(ClasesAlumnosColegio t)
        {
            var res = await _context.ClasesAlumnosColegios.AddAsync(t);
            return res.Entity;
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }

        public ClasesAlumnosColegio Edit(ClasesAlumnosColegio t)
        {
            _context.Entry(t).State = EntityState.Modified;
            return t;
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exist(int ClaseId, int ColegioAlumnoId)
        {
            return await _context.ClasesAlumnosColegios
                .AnyAsync(a => !a.Deleted
                && a.ClaseId == ClaseId
                && a.ColegiosAlumnosId == ColegioAlumnoId);
        }

        public async Task<List<ClasesAlumnosColegio>?> FindAllByClaseIdAndAdminEmail(int ClaseId, string adminEmail = "")
        {
            return await _context.ClasesAlumnosColegios
                .Where(a => !a.Deleted
                && a.ClaseId == ClaseId
                && adminEmail.Equals(a.Clase.Colegio.personas.Email))
                .Include(a => a.ColegiosAlumnos)
                .Include(a => a.ColegiosAlumnos.Persona)
                .ToListAsync();
        }

        public async Task<ClasesAlumnosColegio?> FindByClaseIdAndColegioAlumnoId(int ClaseId, int ColegioAlumnoId)
        {
            return await _context.ClasesAlumnosColegios
                .Include(a => a.Clase)
                .Include(a => a.Clase.Colegio)
                .Include(a => a.Clase.Colegio.personas)
                .FirstOrDefaultAsync(a => !a.Deleted
                && a.ClaseId == ClaseId
                && a.ColegiosAlumnosId == ColegioAlumnoId);
        }

        public async Task<ClasesAlumnosColegio> FindById(int id)
        {
            return await _context.ClasesAlumnosColegios
                    .Where(a => !a.Deleted && a.Id == id)
                    .Include(a => a.Clase)
                    .Include(a => a.ColegiosAlumnos)
                    .Include(a => a.ColegiosAlumnos.Persona)
                    .FirstOrDefaultAsync();
        }

        public IEnumerable<ClasesAlumnosColegio> GetAll(int page, int cantPorPag)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsAlumnoOfClaseAndMateria(int idAlumno, int idMateria)
        {
            return await _context.ClasesAlumnosColegios
                .Include(a => a.Clase)
                .Include(a => a.Clase.MateriaListas)
                .AnyAsync(a => !a.Deleted 
                    && a.Id == idAlumno
                    && a.Clase != null 
                    && !a.Clase.Deleted 
                    && a.Clase.MateriaListas != null
                    && a.Clase.MateriaListas.Any(b => !b.Deleted && b.Id == idMateria));
        }

        public async Task AssingFull(int claseId, int caId, string username)
        {
            var cac = new ClasesAlumnosColegio
            {
                ColegiosAlumnosId = caId,
                ClaseId = claseId,
                CreatedBy = username,
                Created = DateTime.Now,
                Asistencias = new List<Asistencia>()
            };
            
            var materiaListas = await _context.MateriaListas
                .Where(d => !d.Deleted && d.ClaseId == cac.ClaseId)
                .ToListAsync();

            foreach (var ml in materiaListas)
            {
                var fechas = await FilterFecha(ml.Id, username);

                foreach (var fecha in fechas)
                {
                    var asist = new Asistencia()
                    {
                        MateriaListaId = ml.MateriaId,
                        Estado = 'A',
                        Created = DateTime.Now,
                        CreatedBy = username,
                        Fecha = fecha,
                        Alumno = cac
                    };

                    cac.Asistencias.Add(asist);
                }

               
            }
            await _context.ClasesAlumnosColegios.AddAsync(cac);
            await _context.SaveChangesAsync();
        }
        private async Task<List<DateTime>> FilterFecha(int idMateriaLista, string user)
        {
            var query = await _context.Asistencias
                .Where(a => !a.Deleted)
                .Where(a => a.MateriaListaId == idMateriaLista)
                .Select(a => a.Fecha)
                .ToListAsync();

            var fechas = query.Distinct().ToList();

            
            return fechas;
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }
    }
}