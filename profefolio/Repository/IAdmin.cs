using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IAdmin : IDisposable
    {
        Task<IEnumerable<Persona>> GetUsersAssignedOrNotWithRoles(bool band, string role);
    }
}
