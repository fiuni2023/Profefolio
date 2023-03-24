
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using profefolio.Helpers;
using profefolio.Models;
using profefolio.Models.DTOs.Auth;
using profefolio.Models.Entities;
using profefolio.Services;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TestProfefolio.Auth;

public class AuthTest
{
    private Mock<UserManager<Persona>> _userManagerMock = new Mock<UserManager<Persona>>(new Mock<IUserStore<Persona>>().Object, null, null, null, null, null, null, null, null);
    private Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
    private DbContextOptions<ApplicationDbContext> _contextOptions;
    private IConfigurationRoot _configurationBuilder;
    private Mock<TokenGenerator> _tokenGeneratorMock = new Mock<TokenGenerator>();
    private Persona _p;
    private ApplicationDbContext _db;
    private AuthService _authService;

    public AuthTest()
    {
        _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("profefolio")
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
        _db = new ApplicationDbContext(_contextOptions);
        
        LoadData();
        MockSetUp();

        _authService = new AuthService(_userManagerMock.Object, _configurationMock.Object);
    }

    private void LoadData()
    {
        var p = _db.Users.Add(_p).Entity;
        var r = _db.Roles.Add(new IdentityRole()
        {
            Name = "Master"
        }).Entity;

        _db.UserRoles.Add(new IdentityUserRole<string>()
        {
            UserId = p.Id,
            RoleId = r.Id
        });

        _db.SaveChanges();

    }
    private void MockSetUp()
    {
        _configurationMock.Setup(x => x["JWT:Secret"])
            .Returns("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr");

        _configurationMock.Setup(x => x["JWT:ValidIssuer"])
            .Returns("http://localhost:5000");

        _configurationMock.Setup(x => x["JWT:ValidAudience"])
            .Returns("http://localhost:4200");

       
        
        
        var authClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, _p.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        
        authClaims.Add(new Claim(ClaimTypes.Role, "Master"));
        
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationMock.Object["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configurationMock.Object["JWT:ValidIssuer"],
            audience: _configurationMock.Object["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        

        _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(_p);
            
        _userManagerMock.Setup(x => x
            .CheckPasswordAsync(It.IsAny<Persona>(), It.IsAny<string>()))
            .ReturnsAsync(true);
        _userManagerMock.Setup(x => x.GetRolesAsync(It.IsAny<Persona>()))
            .ReturnsAsync(new List<string>()
            {
                "Master"
            });
    }

    [Fact]
    public async Task Login()
    {
        var response = await _authService.Login(new Login()
        {
            Email = "Carlos.Torres123@mail.com",
            Password = "Carlos.Torres123@mail.com"
        });
        
        Assert.NotNull(response);
        Assert.Equal("Carlos.Torres123@mail.com", response.Email);
    }


}