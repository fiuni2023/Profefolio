using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface IEvento : IRepository<Evento>
{
    //Task<Evento> FindByNameEventoId(string nombre, int id);
    //Task<Materia> FindByNameMateria(string nombre);
    Task<List<Evento>> GetAll();
}