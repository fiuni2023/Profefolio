using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface IColegio : IRepository<Colegio>
{
    
    IEnumerable<Colegio> GetAll();
    Task<Colegio> FindByNamePerson(string nombre, string idpersona);
     Task<Persona> FindByPerson(string id);

}