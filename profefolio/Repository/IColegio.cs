using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface IColegio : IRepository<Colegio>
{
    
    IEnumerable<Colegio> GetAll();
}