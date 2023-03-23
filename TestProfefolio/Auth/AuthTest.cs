
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using profefolio.Helpers;
using profefolio.Models;
using profefolio.Models.Entities;

namespace TestProfefolio.Auth;

public class AuthTest
{
    private Mock<UserManager<Persona>> _userManagerMock = new Mock<UserManager<Persona>>();
    private Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
    private string _connectionString = "Server=localhost;Database=TestDb;User Id=testuser;Password=testpassword;";
    private DbContextOptions<ApplicationDbContext> _contextOptions;
    private IConfigurationRoot _configurationBuilder;
    private Mock<TokenGenerator> _tokenGeneratorMock = new Mock<TokenGenerator>();
    private Persona _p;

    public AuthTest()
    {
        _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_connectionString)
            .Options;

        _configurationBuilder = new ConfigurationBuilder().Build();
        _p = new Persona()
        {
            Email = "Carlos.Torres123@mail.com",
            NormalizedEmail = "CARLOS.TORRES123@MAIL.COM",
            Apellido = "Torres",
            Nombre = "Carlos",
            Deleted = false,
            Nacimiento = new DateTime(1999, 07, 10).ToUniversalTime(),
            EsM = true

        };
        var hasher = new PasswordHasher<Persona>();
        _p.PasswordHash = hasher.HashPassword(_p, "Carlos.Torres123");
        


    }

    public void MockSetUp()
    {
        _configurationMock.Setup(x => x["JWT:Secret"])
            .Returns("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr");

        _configurationMock.Setup(x => x["JWT:ValidIssuer"])
            .Returns("http://localhost:5000");

        _configurationMock.Setup(x => x["JWT:ValidAudience"])
            .Returns("http://localhost:4200");

        _userManagerMock.Setup(x => x.CreateAsync(_p));

        _userManagerMock.Setup(x => x.AddToRoleAsync(_p, "Master"));
        
        
        var authClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, _p.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        
        authClaims.Add(new Claim(ClaimTypes.Role, "Master"));
        
        
        

    }

    [Fact]
    public async Task Login()
    {

    }


}