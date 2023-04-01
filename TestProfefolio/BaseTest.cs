using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using profefolio.Models;
using profefolio.Repository;
using profefolio.Services;


namespace TestProfefolio;

public class BaseTest
{
    protected readonly Mock<UserManager<profefolio.Models.Entities.Persona>> UserManagerMock = new Mock<UserManager<profefolio.Models.Entities.Persona>>(new Mock<IUserStore<profefolio.Models.Entities.Persona>>().Object, null, null, null, null, null, null, null, null);
    protected readonly Mock<IConfiguration> ConfigurationMock = new Mock<IConfiguration>();
    protected readonly Mock<IColegio> IColegioMock = new Mock<IColegio>();

    protected readonly profefolio.Models.Entities.Persona P = new profefolio.Models.Entities.Persona()
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
        var hasher = new PasswordHasher<profefolio.Models.Entities.Persona>();
        P.PasswordHash = hasher.HashPassword(P, "Carlos.Torres123");
        Db = new ApplicationDbContext(contextOptions);

        AuthService = new AuthService(UserManagerMock.Object, ConfigurationMock.Object, IColegioMock.Object);
        
        ConfigurationMock.Setup(x => x["JWT:Secret"])
            .Returns("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr");

        ConfigurationMock.Setup(x => x["JWT:ValidIssuer"])
            .Returns("http://localhost:5000");

        ConfigurationMock.Setup(x => x["JWT:ValidAudience"])
            .Returns("http://localhost:4200");
    }

    
}