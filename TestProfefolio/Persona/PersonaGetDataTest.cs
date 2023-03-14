using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using profefolio.Controllers;
using profefolio.Models.DTOs.Persona;
using profefolio.Repository;
namespace TestProfefolio.Persona;

public class PersonaGetDataTest
{
    private static readonly DateTime nacimiento = DateTime.Now;


    [Theory]
    [InlineData("sd65sd6asd46asd4a6s5da6sd4a6s5da6")]
    public async void GetByID_Ok(string id)
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        AccountController controller = new AccountController(mapper.Object, service.Object, rol.Object);

        profefolio.Models.Entities.Persona persona = new profefolio.Models.Entities.Persona()
        {
            Id = "sd65sd6asd46asd4a6s5da6sd4a6s5da6",
            UserName = "RamonRamirez",
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramonramirez@gmail.com",
            EmailConfirmed = true,
            Direccion = "Encarnacion",
            EsM = true,
            Nacimiento = nacimiento,
            Created = nacimiento,
            CreatedBy = "ramonramirez@gmail.com",
            PhoneNumber = "0985123456"
        };

        PersonaResultDTO dto = new PersonaResultDTO()
        {
            Id = "sd65sd6asd46asd4a6s5da6sd4a6s5da6",
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Direccion = "Encarnacion",
            Nacimiento = nacimiento,
            Genero = "M",
            Telefono = "0985123456"
        };

        service.Setup(a => a.FindById(id)).ReturnsAsync(persona);

        mapper.Setup(m => m.Map<PersonaResultDTO>(persona)).Returns(dto);

        var result = await controller.Get(id);

        Assert.IsType<OkObjectResult>(result.Result);
        
    }

    //Contrasenha: Carlos.Torres123
    [Theory]
    [InlineData("Tasdasds")]
    public async void GetById_NotFound(string id)
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();

        var controller = new AccountController(mapper.Object, service.Object, rol.Object);
        

        service.Setup(a => a.FindById(id)).Throws(new FileNotFoundException());
        
        var result = await controller.Get(id);

        Assert.IsType<NotFoundResult>(result.Result);
        
    }
    
    [Theory]
    [InlineData("")]
    public async void GetById_LengthCero(string id)
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();

        AccountController controller = new AccountController(mapper.Object, service.Object, rol.Object);

        profefolio.Models.Entities.Persona persona = new profefolio.Models.Entities.Persona()
        {
            Id = "sd65sd6asd46asd4a6s5da6sd4a6s5da6",
            UserName = "RamonRamirez",
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramonramirez@gmail.com",
            EmailConfirmed = true,
            Direccion = "Encarnacion",
            EsM = true,
            Nacimiento = nacimiento,
            Created = nacimiento,
            CreatedBy = "ramonramirez@gmail.com",
            PhoneNumber = "0985123456"
        };

        service.Setup(a => a.FindById(id)).Throws(new FileNotFoundException());
        
        var result = await controller.Get(id);

        Assert.IsType<NotFoundResult>(result.Result);

    }
    
 
    
}