using profefolio.Models.DTOs.Auth;
using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface IPersona : IRepository<Persona>
{
    Task<Persona> FindById(string id);
    Task<Persona> CreateUser(Persona user, string password);
    Task<Persona> EditProfile(Persona oldPersona, Persona newPersona, string newPassword);
    Task<bool> DeleteUser(string id);

    Task<bool> ExistMail(string email);

    Task<IEnumerable<Persona>> GetAllByRol(string roleName, int page, int cantPorPag);


}