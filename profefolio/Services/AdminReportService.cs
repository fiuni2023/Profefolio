using Microsoft.AspNetCore.Identity;
using profefolio.Models;
using profefolio.Models.Entities;

namespace profefolio
{
    public class AdminReportService : IAdmin
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Persona> _userManager;
        private const string rol = "Administrador de colegio";

        public AdminReportService(ApplicationDbContext db, UserManager<Persona> userManager)
        {
            _db = db;
            _userManager = userManager;
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
            var result = queryPersonas.Where(p => !_db.Colegios.Any(c => c.PersonaId == null ? false : c.PersonaId.Equals(p.Id)));

            //Retornamos la query
            return result;
        }
    }
}