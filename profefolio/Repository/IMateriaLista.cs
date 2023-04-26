using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IMateriaLista : IRepository<MateriaLista>
    {
        Task<bool> IsUsedMateria(int idMateria);
        Task<MateriaLista> Find(int idClase, string idProfesor, int idMateria, string userLogged);
        Task<List<MateriaLista>> FindByIdClase(int idClase, string user);
        Task<bool> DeleteByIdClase(int idClase, string user);

    }
}
