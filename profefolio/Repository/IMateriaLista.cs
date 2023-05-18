using profefolio.Models.DTOs.ClaseMateria;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IMateriaLista : IDisposable
    {
        Task<bool> IsUsedMateria(int idMateria);
        Task<MateriaLista> Find(int idClase, string idProfesor, int idMateria, string userLogged);
        Task<ClaseDetallesDTO> FindByIdClase(int idClase, string user);
        Task<List<MateriaLista>> FindByIdClaseAndUser(int idClase, string userEmail, string role);

    }
}
