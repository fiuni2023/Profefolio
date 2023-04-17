using profefolio.Models.Entities;

namespace profefolio
{
    public interface IAdmin
    {
        Task<IEnumerable<Persona>> GetPersonasSinColegio(int cantPerPage, int page);
        Task<IEnumerable<Persona>> GetPersonasConColegio(int cantPerPage, int page);
        Task<int> Count(bool band);
    }
}