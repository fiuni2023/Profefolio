using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface IPersona : IRepository<Persona>
{
    Task<Persona> FindById(string id);
    Task<Persona> CreateUser(Persona user, string password);
    Task<Persona> EditProfile(Persona p);
    Task<bool> DeleteUser(string id);
    Task<IEnumerable<Persona>> FilterByRol(int page, int cantPorPag, string rol);
    Task<bool> ChangePassword(Persona p, string newPassword);
    Task<bool> ExistMail(string email);
    Task<IEnumerable<Persona>> GetAllByRol(string roleName, int page, int cantPorPag);
    Task<int> CountByRol(string rol);
    Task<IEnumerable<Persona>> GetAllByRol(string roleName);
    Task<bool> ExistDoc(Persona persona);
    Task<Persona> FindByEmail(string email);
    Task<IList<string>> GetRolesPersona(Persona user);
    Task<Persona> FindByIdAndRole(string id, string role);
    Task<bool> DeleteByUserAndRole(string id, string role);
    Task<Persona?> FindByDocumentoAndRole(string documento, string role);    

}