using profefolio.Models.DTOs.Auth;
using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface IPersona : IRepository<Persona>
{
    Task<Persona> FindById(string id);
    Task<Persona> CreateUser(Persona user, string password);
    Task<Persona> EditProfile(string id, Persona persona);
    Task<bool> DeleteUser(string id);
    Task<bool> ChangePassword(string id, ModelPassword newPassoword);
    Task<bool> ExistMail(string email);

}