using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface IMateria : IRepository<Materia>
{
    Task<Materia> FindByNameMateriaId(string nombre, int id);
    Task<Materia> FindByNameMateria(string nombre);
    Task<List<Materia>> FindAllUnsignedMaterias(int idClase);
    Task<List<Materia>> GetAll();
}