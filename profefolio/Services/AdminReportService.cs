using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;
namespace profefolio
{
    public class AdminReportService : IAdmin
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Persona> _userManager;
        private readonly IPersona _personasService;
        private const string rol = "Administrador de colegio";

        public AdminReportService(ApplicationDbContext db, UserManager<Persona> userManager,IPersona personasService)
        {
            _db = db;
            _userManager = userManager;
            _personasService = personasService;
        }

        public async Task<IEnumerable<Persona>> GetPersonasSinColegio(int cantPerPage, int page)
        {

            //Filtramos las personas por el rol
            var queryPersonas = await _userManager.GetUsersInRoleAsync(rol);

            //Filtramos por aquellas que no han sido borradas
            queryPersonas = queryPersonas
            .Where(p => !p.Deleted)
            .Skip(cantPerPage * page)
            .Take(cantPerPage)
            .ToList();

            //Creamos otra query donde obtenemos las personas cuyos Id de Persona en la tabla Colegio es NULL
            var result = queryPersonas
                .Where(p => !_db.Colegios
                    .Any(c => c.PersonaId == null ?
                        false : c.PersonaId.Equals(p.Id)));

            //Retornamos la query
            return result;
        }

        public async Task<IEnumerable<Persona>> GetPersonasConColegio(int cantPerPage, int page)
        {
            var query = _db.Colegios
                            .Include(c => c.personas)
                            .Where(c => !c.Deleted)
                            .Select(c => c.personas)
                            .Where(p => !p.Deleted);

            var personas = await query
                                .Skip(cantPerPage * page)
                                .Take(cantPerPage)
                                .ToListAsync();

            return personas;
        }

        public async Task<int> Count(bool band)
        {
            if (band)
            {
                var queryPersona = _db.Colegios
                            .Include(c => c.personas)
                            .Where(c => !c.Deleted)
                            .Select(c => c.personas)
                            .Where(p => !p.Deleted);

                var personas = await queryPersona
                                    .ToListAsync();

                return personas.Count;
            }
            var query = await _userManager.GetUsersInRoleAsync(rol);

            query = query
                .Where(p => !p.Deleted)
                .ToList();


            var result = query
            .Where(p => !_db.Colegios
                .Any(c => c.PersonaId == null ?
                    false : c.PersonaId.Equals(p.Id)))
            .Count();

            return result;

        }

         public async Task<IEnumerable<Persona>> GetAdminsSinColegio()
        {
            var admins = await _personasService.GetAllByRol(rol);
            

            var colegios = await _db.Colegios.Where(a => !a.Deleted).ToListAsync();

            //left join
            var result = from a in admins
                            join c in colegios
                            on a.Id equals c.PersonaId into joinlist
                            from col in joinlist.DefaultIfEmpty()
                            where col is null
                            select a;
            return result;
        }

    }
}