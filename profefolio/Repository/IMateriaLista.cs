using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IMateriaLista : IRepository<MateriaLista>
    {
        IEnumerable<MateriaLista> FilterByIdMateriaAndUserAndClass(int idMateria, string createdBy, int idClase);
    }
}
