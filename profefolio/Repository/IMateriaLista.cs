using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IMateriaLista : IRepository<MateriaLista>
    {
        IEnumerable<MateriaLista> FilterByIdMateriaAndUserAndClass(int idMateria, string createdBy, int idClase);


        Task<bool> IsUsedMateria(int idMateria);

        Task<IEnumerable<MateriaLista>> GetDetalleClaseByIdMateriaAndUsername(string username, int idMateria);

    }
}
