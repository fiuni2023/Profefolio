using profefolio.Models.DTOs.Auth;
using profefolio.Repository;

namespace profefolio.Services;

public class AuthService : IAuth
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task<AuthPersonaDTO> Login(Login login)
    {
        throw new NotImplementedException();
    }

    public Task Logout()
    {
        throw new NotImplementedException();
    }
}