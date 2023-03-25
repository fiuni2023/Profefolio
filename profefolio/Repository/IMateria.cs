using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface IMateria : IRepository<Materia>
{
    Task<Materia> FindByNameMateria(string nombre);
}