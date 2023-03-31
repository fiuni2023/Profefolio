using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using profefolio.Models.DTOs.Auth;
using profefolio.Models.Entities;


namespace TestProfefolio.Auth;

public class AuthTest : BaseTest
{

    public AuthTest()
    {
        LoadData();
    }
    
    [Fact]
    public async Task LoginServiceOkTest()
    {
        MockSetUpOk();
        var response = await AuthService.Login(new Login()
        {
            Email = "Carlos.Torres123@mail.com",
            Password = "Carlos.Torres123@mail.com"
        });
        
        Assert.NotNull(response);
        Assert.Equal("Carlos.Torres123@mail.com", response.Email);
    }

    [Fact]
    public async Task LoginServiceIncorrectPasswordTest()
    {
        MockSetUpIncorrectPassword();
        var ex = await Assert.ThrowsAsync<BadHttpRequestException>(() =>

          AuthService.Login(new Login()
          {
              Email = "Carlos.Torres123@mail.com",
              Password = "Carlos.Torres@mail.com"
          }));
        Assert.Equal("Credenciales no validas", ex.Message);

    }

    internal void LoadData()
    {
        var p = Db.Users.Add(P).Entity;
        var r = Db.Roles.Add(new IdentityRole()
        {
            Name = "Master"
        }).Entity;

        Db.UserRoles.Add(new IdentityUserRole<string>()
        {
            UserId = p.Id,
            RoleId = r.Id
        });
        
        
        Db.SaveChanges();

    }

    private void MockSetUpOk()
    {
        UserManagerMock.Setup(x => x.FindByEmailAsync("Carlos.Torres123@mail.com")).ReturnsAsync(P);

        UserManagerMock.Setup(x => x
                .CheckPasswordAsync(It.IsAny<Persona>(), It.IsAny<string>()))
            .ReturnsAsync(true);
        UserManagerMock.Setup(x => x.GetRolesAsync(It.IsAny<Persona>()))
            .ReturnsAsync(new List<string>()
            {
                "Master"
            });
    }

    private void MockSetUpIncorrectPassword()
    {
        UserManagerMock.Setup(x => x.FindByEmailAsync("Carlos.Torres123@mail.com"))
        .ReturnsAsync(P);

        UserManagerMock.Setup(x => x
                .CheckPasswordAsync(It.IsAny<Persona>(), It.IsAny<string>()))
            .ReturnsAsync(false);
        
    }
  
}