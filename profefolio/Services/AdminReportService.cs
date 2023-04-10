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

        public IEnumerable<Persona> GetUsersAssignedOrNotWithRoles(bool band, string role)
        {
            var queryBase = _db.Colegios
                    .Include(c => c.personas);
                    
            if (band)
            {
                return queryBase
                    .Where(c => !c.Deleted && c.personas != null)
                    .Select(p => p.personas)
                    .Where(p => !p.Deleted)
                    .Where( p =>  _userManager.IsInRoleAsync(p, role).Result);    
            }

            return queryBase;

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
