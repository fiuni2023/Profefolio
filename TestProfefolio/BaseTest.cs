using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Services;

namespace TestProfefolio;

public class BaseTest
{
    protected readonly Mock<UserManager<Persona>> UserManagerMock = new Mock<UserManager<Persona>>(new Mock<IUserStore<Persona>>().Object, null, null, null, null, null, null, null, null);
    protected readonly Mock<IConfiguration> ConfigurationMock = new Mock<IConfiguration>();
    protected readonly Persona P = new Persona()
    {
        Email = "Carlos.Torres123@mail.com",
        NormalizedEmail = "CARLOS.TORRES123@MAIL.COM",
        Apellido = "Torres",
        Nombre = "Carlos",
        Deleted = false,
        Nacimiento = new DateTime(1999, 07, 10).ToUniversalTime(),
        EsM = true

    };
    public readonly ApplicationDbContext Db;
    public AuthService AuthService { get; set; }


    protected BaseTest()
    {
        var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("profefolio")
            .Options;
        var hasher = new PasswordHasher<Persona>();
        P.PasswordHash = hasher.HashPassword(P, "Carlos.Torres123");
        Db = new ApplicationDbContext(contextOptions);

        AuthService = new AuthService(UserManagerMock.Object, ConfigurationMock.Object);
        
        ConfigurationMock.Setup(x => x["JWT:Secret"])
            .Returns("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr");

        ConfigurationMock.Setup(x => x["JWT:ValidIssuer"])
            .Returns("http://localhost:5000");

        ConfigurationMock.Setup(x => x["JWT:ValidAudience"])
            .Returns("http://localhost:4200");
    }

    
}