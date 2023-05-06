using profefolio.Models.Entities;
using profefolio.Models.DTOs.Persona;
namespace profefolio.Repository;

public interface IColegio : IRepository<Colegio>
{
    
    IEnumerable<Colegio> GetAll();
    Task<Colegio> FindByNamePerson(string nombre, string idpersona);
    Task<Colegio> FindByNameColegio(string nombre);
    Task<Persona> FindByPerson(string id);
    Task<int> FindByPersonRol(string id);
    Task<Colegio> FindByIdAdmin(string id);
    Task<bool> ExistOtherWithEqualName(string newName, int id);
    Task<bool> ExistAdminInOtherColegio(string idNewAdmin, int idColegio);
}