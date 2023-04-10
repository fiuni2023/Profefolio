using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services
{
    public class AdminReportService : IAdmin
    {
        private bool disposedValue;

        private readonly ApplicationDbContext _db;
        private readonly UserManager<Persona> _userManager;

        public AdminReportService(ApplicationDbContext db, UserManager<Persona> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IEnumerable<Persona> GetUsersAssignedOrNotWithRoles(bool band)
        {

            var colegioQuery = _db.Colegios.Where(c => !c.Deleted).Include(c => c.personas);
            var personaQuery = _db.Users.Where(p => !p.Deleted);
                    
            if (band)
            {
                return colegioQuery
                    .Where(c => c.personas != null)
                    .Select(p => p.personas)
                    .Where(p => !p.Deleted)
                    .Where( p =>  _userManager.IsInRoleAsync(p, "Administrador de Colegio").Result);    
            }

            return personaQuery
                .Where(p => colegioQuery
                    .Any(c => c.personas != null && c.personas.Id.Equals(p.Id))
                 )
                .Where(p => _userManager.IsInRoleAsync(p, "Administrador de Colegio").Result);

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {

            // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
