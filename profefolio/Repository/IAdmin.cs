using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IAdmin : IDisposable
    {
        IEnumerable<Persona> GetUsersAssignedOrNotWithRoles(bool band);
    }
}
