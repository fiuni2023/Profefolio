using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services
{
    public class AdminReportService : IAdmin
    {
        private bool disposedValue;

        private readonly ApplicationDbContext _db;

        public AdminReportService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<IEnumerable<Persona>> GetUsersAssignedOrNotWithRoles(bool band, string role)
        {
            throw new NotImplementedException();
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
