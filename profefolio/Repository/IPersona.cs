using profefolio.Models.DTOs.Auth;
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


}