using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface IEvento : IDisposable
{
    Task<Evaluacion> Add(Evaluacion e, string user);
    Task Save();
}