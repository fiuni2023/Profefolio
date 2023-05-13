using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface IEvento : IRepository<Evento>
{
    Task<Evento> FindByEventoRepetido(String tipo, DateTime fecha, int clase, int materia, int colegio);
    //Task<List<Evento>> GetAll();
}