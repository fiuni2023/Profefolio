using profefolio.Models.Entities;
using profefolio.Models.DTOs.Persona;
namespace profefolio.Repository;

public interface IColegio : IRepository<Colegio>
{
    
    IEnumerable<Colegio> GetAll();
    Task<Colegio> FindByNamePerson(string nombre, string idpersona);
     Task<Persona> FindByPerson(string id);

}