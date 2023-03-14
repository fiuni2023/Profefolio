using profefolio.Models.DTOs.Auth;

namespace profefolio.Repository;

public interface IAuth: IDisposable
{
   Task<AuthPersonaDTO> Login(Login login);
   Task Logout();
}