using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface IEvento : IDisposable
{
    Task<Evaluacion> FindByEventoRepetido(String tipo, DateTime fecha, int clase, int materia, int colegio);
    Task<List<Evaluacion>> GetAll(String prfId);
    Task<Evaluacion> FindById2(int id);
    Task<Evaluacion> Add(Evaluacion e, string user);
    Evaluacion Edit(Evaluacion e);
    Task Save();
    int Count();
}