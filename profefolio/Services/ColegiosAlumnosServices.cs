using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services
{
    public class ColegiosAlumnosServices : IColegiosAlumnos
    {
        private ApplicationDbContext _context;
        public ColegiosAlumnosServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ColegiosAlumnos> Add(ColegiosAlumnos t)
        {
            var result = await _context.ColegiosAlumnos.AddAsync(t);
            return result.Entity;
        }

        /*
            Retorna la cantidad de alumnos de un colegio
        */
        public async Task<int> Count(int idColegio)
        {
            return await _context.ColegiosAlumnos
                            .CountAsync(ca => !ca.Deleted && ca.ColegioId == idColegio);
        }

        public async Task<int> Count(string adminEmail)
        {
            return await _context.ColegiosAlumnos.CountAsync(a => !a.Deleted && adminEmail.Equals(a.Colegio.personas.Email));
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountNotAssigned(string user, int idClase)
        {
            var query = await this.FindAllNoAssignedToClaseByEmailAdminAndIdClase(user, idClase);
            return query.Count();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public ColegiosAlumnos Edit(ColegiosAlumnos t)
        {
            _context.Entry(t).State = EntityState.Modified;
            return t;
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exist(string idAlumno, int idColegio)
        {
            return await _context.ColegiosAlumnos.AnyAsync(ca => !ca.Deleted
                    && ca.ColegioId == idColegio
                    && ca.PersonaId.Equals(idAlumno));
        }

        public async Task<IEnumerable<ColegiosAlumnos>> FindAllByAdminEmail(int page, int cantPorPag, string adminEmail)
        {
            return await _context.ColegiosAlumnos
                    .Where(a => !a.Deleted
                        && adminEmail.Equals(a.Colegio.personas.Email))
                    .OrderByDescending(ca => ca.Id)
                    .Skip(page * cantPorPag)
                    .Take(cantPorPag)
                    .Include(a => a.Persona)
                    .ToListAsync();
        }


        public async Task<IEnumerable<ColegiosAlumnos>> FindAllByIdColegio(int page, int cantPorPag, int idColegio)
        {
            return await _context.ColegiosAlumnos
                    .Include(a => a.Persona)
                    .Where(ca => !ca.Deleted && ca.ColegioId == idColegio)
                    .OrderByDescending(ca => ca.Id)
                    .Skip(page * cantPorPag)
                    .Take(page)
                    
                    .ToListAsync();
        }

        public async Task<IEnumerable<ColegiosAlumnos>> FindAllNoAssignedToClaseByEmailAdminAndIdClase(string adminEmail, int idClase)
        {
            var clase = await _context.Clases
                    .AnyAsync(c => !c.Deleted && c.Id == idClase);

            if (!clase)
            {
                throw new BadHttpRequestException("clase no valida");
            }

            var query = await _context.ColegiosAlumnos
                        .Where(ca => !ca.Deleted)
                        .Where(ca => ca.Colegio.personas.Email.Equals(adminEmail))
                        .Where(ca => ca.ClasesAlumnosColegios == null ||
                            !ca.ClasesAlumnosColegios.Any() ||
                                ca.ClasesAlumnosColegios.Any(a => (a.Deleted && a.ClaseId == idClase) || a.ClaseId != idClase))
                        .Where(ca => ca.ClasesAlumnosColegios == null || !(ca.ClasesAlumnosColegios.Any(a => a.ClaseId == idClase && !a.Deleted)))
                        .Include(a => a.Persona)
                        .Include(a => a.ClasesAlumnosColegios)
                        .ToListAsync();
            return query;
        }

        public async Task<ColegiosAlumnos> FindById(int id)
        {
            return await _context.ColegiosAlumnos
                .Include(a => a.Colegio)
                .Include(a => a.Colegio.personas)
                .FirstAsync(ca => !ca.Deleted && ca.Id == id);
        }

        public async Task<IEnumerable<ColegiosAlumnos>> FindNotAssigned(string user, int idClase, int page, int cantPerPage)
        {
            var existClase = _context.Clases
                .Any(x => !x.Deleted && x.Id == idClase);

            if (!existClase)
            {
                throw new FileNotFoundException();
            }
            var query = await this.FindAllNoAssignedToClaseByEmailAdminAndIdClase(user, idClase);

            var result = query
                .Skip(cantPerPage * page)
                .Take(cantPerPage);


            return result;
        }

        public async Task<IEnumerable<ColegiosAlumnos>> FindAll(string user, int page, int cantPerPage)
        {
            var userLogged = await _context.Users
                .Where(t => !t.Deleted)
                .Where(t => t.Email.Equals(user))
                .FirstOrDefaultAsync();

            if (userLogged == null)
            {
                throw new BadHttpRequestException("Admin no valido");
            }

            var colegio = await _context.Colegios
                .Include(t => t.personas)
                .Where(t => !t.Deleted)
                .Where(t => t.personas.Id.Equals(userLogged.Id))
                .FirstOrDefaultAsync();

            if (colegio == null)
            {
                throw new BadHttpRequestException("Colegio no valido");
            }

            var colegiosAlumnos = _context.ColegiosAlumnos
                .Include(ca => ca.Colegio)
                .Include(ca => ca.Persona)
                .Where(ca => !ca.Deleted && ca.ColegioId == colegio.Id)
                .OrderBy(ca => ca.Persona.Apellido)
                .ThenBy(ca => ca.Persona.Nombre)
                .ThenBy(ca => ca.Persona.Documento)
                .Skip(cantPerPage * page)
                .Take(cantPerPage);

            return colegiosAlumnos;
        }

        public IEnumerable<ColegiosAlumnos> GetAll(int page, int cantPorPag)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ColegiosAlumnos>> FindNotAssignedByYear(string user, int idClase, int page, int cantPerPage)
        {
            var date = DateTime.Now;

            var year = date.Year;

            var query = await GetNotAssignedByYear(year, user, idClase);

            return query.Skip(page * cantPerPage).Take(cantPerPage);

        }

        public async Task<IEnumerable<ColegiosAlumnos>> GetNotAssignedByYear(int year, string user, int idClase)
        {
            //Resultado
            var list = new List<ColegiosAlumnos>();
            
            //Persona Logeada
            var us = await _context.Users
                .Where(u => !u.Deleted && u.Email.Equals(user))
                .FirstOrDefaultAsync();

            if (us == null)
            {
                throw new UnauthorizedAccessException();
            }
            //Mi Colegio
            var colegio = await _context.Colegios
                .Include(c => c.ColegiosAlumnos)
                .ThenInclude(p => p.Persona)
                .Where(c => c.PersonaId != null && !c.Deleted && c.PersonaId.Equals(us.Id))
                .FirstOrDefaultAsync();

            if (colegio == null)
            {
                throw new UnauthorizedAccessException();
            }

            //Relacion entre Alumnos y Colegio
            var colegioAlumnosQuery = colegio.ColegiosAlumnos
                .Where(x => !x.Deleted);
            
            foreach (var colegioAlumno in colegioAlumnosQuery)
            {

                //Relacion de ColegioALumno a Clase
                var claseAlumnoColegioQuery = await _context.ClasesAlumnosColegios
                    .Include(cl => cl.Clase)
                    .Where(cac => cac.Clase != null && !cac.Deleted && cac.ColegiosAlumnosId == colegioAlumno.Id && cac.Clase.Anho == year)
                    .ToListAsync();

                if (!claseAlumnoColegioQuery.Any())
                {
                    list.Add(colegioAlumno);
                    
                }
                
            }


            return list;



        }

        public async Task<int> ContNotAssignedByYear(int year, string user, int idClase)
        {
            var query = await this.GetNotAssignedByYear(year, user, idClase);

            return query.Count();
        }
    }
}