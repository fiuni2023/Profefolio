using profefolio.Models.DTOs.ClaseMateria;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IMateriaLista : IRepository<MateriaLista>
    {
        Task<bool> IsUsedMateria(int idMateria);
        Task<MateriaLista> Find(int idClase, string idProfesor, int idMateria, string userLogged);
        Task<ClaseDetallesDTO> FindByIdClase(int idClase, string user);
        Task<List<MateriaLista>> FindByIdClaseAndUser(int idClase, string userEmail, string role);
        Task<bool> DeleteByIdClase(int idClase, string user);
        Task<bool> SaveMateriaLista(ClaseMateriaCreateDTO dto, string user);
        Task<bool> EditMateriaLista(ClaseMateriaEditDTO dto, string user);

    }
}
