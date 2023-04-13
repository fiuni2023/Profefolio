using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IMateriaLista : IRepository<MateriaLista>
    {
        IEnumerable<MateriaLista> FilterByIdMateriaAndUser(int idMateria, string createdBy);
    }
}
