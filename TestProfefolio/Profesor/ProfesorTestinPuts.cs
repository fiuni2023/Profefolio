using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using profefolio.Controllers;
using profefolio.Models.DTOs.Persona;
using profefolio.Repository;

namespace TestProfefolio.Profesor;

public class ProfesorTestinPuts
{
    private static readonly DateTime nacimiento = DateTime.Now;

    [Fact]
    public async void Put_Ok()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);

        string id = "sd65sd6asd46asd4a6s5da6sd4a6s5da6";

        profefolio.Models.Entities.Persona personaOld = new profefolio.Models.Entities.Persona()
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
            PhoneNumber = "0985123456"
        };

        profefolio.Models.Entities.Persona personaNew = new profefolio.Models.Entities.Persona()
        {
            Id = "sd65sd6asd46asd4a6s5da6sd4a6s5da7",
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
            PhoneNumber = "0985123450"
        };


        PersonaDTO personaDtoNew = new PersonaDTO()
        {
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramonramirez@gmail.com",
            Genero = "M",
            Nacimiento = nacimiento,
            Telefono = "0985123450",
            Password = "12345678",
            ConfirmPassword = "12345678"
        };

        PersonaResultDTO dtoResult = new PersonaResultDTO()
        {
            Id = "sd65sd6asd46asd4a6s5da6sd4a6s5da7",
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Genero = "M",
            Nacimiento = nacimiento,
            Telefono = "0985123450"
        };


        service.Setup(p => p.FindById(id)).ReturnsAsync(personaOld);

        mapper.Setup(m => m.Map<profefolio.Models.Entities.Persona>(personaDtoNew)).Returns(personaNew);

        service.Setup(s => s.EditProfile(personaOld, personaNew, personaDtoNew.Password)).ReturnsAsync(personaNew);

        mapper.Setup(m => m.Map<PersonaResultDTO>(personaNew)).Returns(dtoResult);


        var result = await controller.Put(id, personaDtoNew);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async void Put_NotFound_Id()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);

        string id = "sd65sd6asd46asd4a6s5da6sd4a6s5da6";

        profefolio.Models.Entities.Persona personaOld = new profefolio.Models.Entities.Persona()
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
            PhoneNumber = "0985123456"
        };

        profefolio.Models.Entities.Persona personaNew = new profefolio.Models.Entities.Persona()
        {
            Id = "sd65sd6asd46asd4a6s5da6sd4a6s5da7",
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
            PhoneNumber = "0985123450"
        };


        PersonaDTO personaDtoNew = new PersonaDTO()
        {
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramonramirez@gmail.com",
            Genero = "M",
            Nacimiento = nacimiento,
            Telefono = "0985123450",
            Password = "12345678",
            ConfirmPassword = "12345678"
        };

        PersonaResultDTO dtoResult = new PersonaResultDTO()
        {
            Id = "sd65sd6asd46asd4a6s5da6sd4a6s5da7",
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Genero = "M",
            Nacimiento = nacimiento,
            Telefono = "0985123450"
        };


        service.Setup(p => p.FindById(id)).Throws(new FileNotFoundException());


        var result = await controller.Put(id, personaDtoNew);

        Assert.IsType<NotFoundObjectResult>(result);
    }


    [Fact]
    public async void Put_BadRequest_EmailNewExist()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);

        string id = "sd65sd6asd46asd4a6s5da6sd4a6s5da6";

        profefolio.Models.Entities.Persona personaOld = new profefolio.Models.Entities.Persona()
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
            PhoneNumber = "0985123456"
        };

        profefolio.Models.Entities.Persona personaNew = new profefolio.Models.Entities.Persona()
        {
            Id = "sd65sd6asd46asd4a6s5da6sd4a6s5da7",
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
            PhoneNumber = "0985123450"
        };


        PersonaDTO personaDtoNew = new PersonaDTO()
        {
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramonramirez@gmail.com",
            Genero = "M",
            Nacimiento = nacimiento,
            Telefono = "0985123450",
            Password = "12345678",
            ConfirmPassword = "12345678"
        };

        PersonaResultDTO dtoResult = new PersonaResultDTO()
        {
            Id = "sd65sd6asd46asd4a6s5da6sd4a6s5da7",
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Genero = "M",
            Nacimiento = nacimiento,
            Telefono = "0985123450"
        };


        service.Setup(p => p.FindById(id)).ReturnsAsync(personaOld);

        mapper.Setup(m => m.Map<profefolio.Models.Entities.Persona>(personaDtoNew)).Returns(personaNew);

        service.Setup(s => s.EditProfile(personaOld, personaNew, personaDtoNew.Password))
            .Throws(new BadHttpRequestException("El email que desea actualizar ya existe"));

        mapper.Setup(m => m.Map<PersonaResultDTO>(personaNew)).Returns(dtoResult);


        var result = await controller.Put(id, personaDtoNew);
        BadRequestObjectResult r = (BadRequestObjectResult)result.Result;
        
        Assert.Equal("El email que desea actualizar ya existe", r.Value.ToString());
    }

    [Fact]
    public async void Post_BadRequest_PasswordNull()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);


        PersonaDTO personaDto = new PersonaDTO()
        {
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramonramirez@gmail.com",
            Genero = "M",
            Nacimiento = nacimiento,
            Telefono = "0985123456",
            Password = null,
            ConfirmPassword = "12345678"
        };

        ActionResult<PersonaResultDTO> result = (await controller.Post(personaDto));

        BadRequestObjectResult badRequestObjectResult = (BadRequestObjectResult)result.Result;
        Assert.Equal("Falta el Password", badRequestObjectResult.Value.ToString());
    }
}