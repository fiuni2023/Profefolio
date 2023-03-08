using profefolio.Controllers;
using profefolio.Models.DTOs.Auth;
using profefolio.Repository;

namespace TestProfefolio.Auth;

public class AuthResultValueTest
{
    private readonly Mock<IAuth> _authService = new Mock<IAuth>();
    private readonly AuthController _authController;


    public AuthResultValueTest()
    {
        _authController = new AuthController(_authService.Object);
    }
    
    
    [Fact]
    public async Task LoginResponseValueTest()
    {
        var login = new Login()
        {
            Email = "Carlos.Torres123@mail.com",
            Password = "Carlos.Torres123"
        };

        var persona = new AuthPersonaDTO()
        {
            Email = "Carlos.Torres123@mail.com",
            Roles = new List<string>()
            {
                "Master"
            }
        };

        _authService.Setup(s => s.Login(login))
            .Returns(Task.FromResult(persona));

        var result = await _authController.Login(login);

        Assert.IsType<AuthPersonaDTO>(result.Value);
    }
}