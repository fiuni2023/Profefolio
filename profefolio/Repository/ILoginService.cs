using profefolio.Models.DTOs.Auth;

namespace profefolio.Repository;

public interface ILoginService : IDisposable
{
   Task<AuthPersonaDTO> Login(Login login);
   Task Logout();
}