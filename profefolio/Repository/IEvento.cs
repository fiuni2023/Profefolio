using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface IEvento : IRepository<Evaluacion>
{
    Task<Evaluacion> FindByEventoRepetido(String tipo, DateTime fecha, int clase, int materia, int colegio);
    Task<List<Evaluacion>> GetAll(String prfId);
    Task<Evaluacion> FindById2(int id);
}