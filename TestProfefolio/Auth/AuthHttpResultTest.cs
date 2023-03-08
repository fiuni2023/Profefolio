using Microsoft.AspNetCore.Mvc;
using profefolio.Controllers;
using profefolio.Models.DTOs.Auth;
using profefolio.Repository;

namespace TestProfefolio.Auth;

public class AuthHttpResultTest
{
    private readonly Mock<IAuth> _authService = new Mock<IAuth>();
    private readonly AuthController _authController;


    public AuthHttpResultTest()
    {
        _authController = new AuthController(_authService.Object);
    }
    [Fact]
    public async Task LoginResponseOkTest()
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
            .ReturnsAsync(persona);

        var result = await _authController.Login(login);

        Assert.IsType<OkObjectResult>(result.Result);
    }


    [Fact]
    public async Task InvalidModelStateLoginTest()
    {
        var login = new Login()
        {
            Email = "Carlos.Torres123@mail.com"
        };

        _authController.ModelState.AddModelError("error", "error");

        var result = await _authController.Login(login);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task InvalidCredentials()
    {
        var login = new Login()
        {
            Email = "Carlos.Torres123@mail.com",
            Password = "Carlos.Torres"
        };

        _authService.Setup(s => s.Login(login))
            .Throws<Exception>();
        

        _authController.ModelState.AddModelError("error", "error");

        var result = await _authController.Login(login);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

}