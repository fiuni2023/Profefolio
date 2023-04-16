using profefolio.Models.Entities;

namespace profefolio
{
    public interface IAdmin
    {
        Task<IEnumerable<Persona>> GetPersonasSinColegio(int cantPerPage, int page);
    }
}