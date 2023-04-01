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

    [Fact]
    public async Task LoginServiceNotFoundUser()
    {
        MockSetUpNotFoundUser();

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
        var personaMaster= Db.Users.Add(P).Entity;

        var personaAdministrador = new Persona();
        personaAdministrador.Nombre = "Edgar";
        personaAdministrador.Apellido = "Allan";
        personaAdministrador.Created = DateTime.Now;
        personaAdministrador.EsM = true;
        personaAdministrador.Email = "Edgar.Allan123@mail.com";
        personaAdministrador.Direccion = "avda123";
        personaAdministrador.CreatedBy = "me";
        var hasher = new PasswordHasher<Persona>();
        personaAdministrador.PasswordHash = hasher.HashPassword(personaAdministrador, "Edgar.Allan123");

        var personaAdministradorEnt = Db.Users.Add(personaAdministrador).Entity;

        var rolMaster = Db.Roles.Add(new IdentityRole()
        {
            Name = "Master"
        }).Entity;

        Db.UserRoles.Add(new IdentityUserRole<string>()
        {
            UserId = personaMaster.Id,
            RoleId = rolMaster.Id
        });

        var colegio = new Colegio();
        colegio.Created = DateTime.Now;
        colegio.CreatedBy = "Me";
        colegio.Nombre = "CNDEN";
        colegio.PersonaId = personaAdministradorEnt.Id;
        
        
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
    private void MockSetUpNotFoundUser()
    {
        UserManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((Persona)null);

    }
  
}