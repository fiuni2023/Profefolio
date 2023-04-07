using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using profefolio.Controllers;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.Entities;
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


        PersonaEditDTO personaDtoNew = new PersonaEditDTO()
        {
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramonramirez@gmail.com",
            Genero = "M",
            Nacimiento = nacimiento,
            Telefono = "0985123450"
        };

        PersonaResultDTO dtoResult = new PersonaResultDTO()
        {
            Id = "sd65sd6asd46asd4a6s5da6sd4a6s5da6",
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Genero = "M",
            Nacimiento = nacimiento,
            Telefono = "0985123450",
            Email = "ramonramirez@gmail.com"
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "user@gmail.com")
            }, "role"));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        service.Setup(p => p.FindById(It.IsAny<string>())).ReturnsAsync(personaOld);

        service.Setup(p => p.ExistMail(It.IsAny<string>())).ReturnsAsync(true);

        service.Setup(s => s.EditProfile(It.IsAny<Persona>())).ReturnsAsync(personaNew);

        mapper.Setup(m => m.Map<PersonaResultDTO>(It.IsAny<Persona>())).Returns(dtoResult);


        var result = await controller.Put(id, personaDtoNew);



        var jsonResult = Assert.IsType<OkObjectResult>(result.Result);
        var objectResult = Assert.IsType<PersonaResultDTO>(jsonResult.Value);

        Assert.Equal(dtoResult.Id, objectResult.Id);
        Assert.Equal(dtoResult.Nombre, objectResult.Nombre);
        Assert.Equal(dtoResult.Apellido, objectResult.Apellido);
        Assert.Equal(dtoResult.Nacimiento, objectResult.Nacimiento);
        Assert.Equal(dtoResult.Direccion, objectResult.Direccion);
        Assert.Equal(dtoResult.Documento, objectResult.Documento);
        Assert.Equal(dtoResult.DocumentoTipo, objectResult.DocumentoTipo);
        Assert.Equal(dtoResult.Telefono, objectResult.Telefono);
        Assert.Equal(dtoResult.Email, objectResult.Email);
        Assert.Equal(dtoResult.Genero, objectResult.Genero);

    }



    //model invalid
    [Fact]
    public async void Put_ModelError_BadRequest()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);
        controller.ModelState.AddModelError("", "");

        string id = "sd65sd6asd46asd4a6s5da6sd4a6s5da6";

        PersonaEditDTO personaDtoNew = new PersonaEditDTO()
        {
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramonramirez@gmail.com",
            Genero = "M",
            Nacimiento = nacimiento,
            Telefono = "0985123450"
        };


        var result = await controller.Put(id, personaDtoNew);

        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);
    }



    //date of birth
    [Fact]
    public async void Put_DateOfBirthInvalid_BadRequest()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);

        string id = "sd65sd6asd46asd4a6s5da6sd4a6s5da6";

        PersonaEditDTO personaDtoNew = new PersonaEditDTO()
        {
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramonramirez@gmail.com",
            Genero = "M",
            Nacimiento = nacimiento.AddDays(3),
            Telefono = "0985123450"
        };


        var result = await controller.Put(id, personaDtoNew);

        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("El nacimiento no puede ser mayor a la fecha de hoy", jsonResult.Value);
    }



    // gender null
    [Fact]
    public async void Put_GenderNull_BadRequest()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);

        string id = "sd65sd6asd46asd4a6s5da6sd4a6s5da6";

        PersonaEditDTO personaDtoNew = new PersonaEditDTO()
        {
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramonramirez@gmail.com",
            Genero = null,
            Nacimiento = nacimiento,
            Telefono = "0985123450"
        };


        var result = await controller.Put(id, personaDtoNew);

        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Se tiene que incluir el genero", jsonResult.Value);
    }



    // gender distinct to M or F
    [Fact]
    public async void Put_GenderInvalid_BadRequest()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);

        string id = "sd65sd6asd46asd4a6s5da6sd4a6s5da6";

        PersonaEditDTO personaDtoNew = new PersonaEditDTO()
        {
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramonramirez@gmail.com",
            Genero = "X",
            Nacimiento = nacimiento,
            Telefono = "0985123450"
        };


        var result = await controller.Put(id, personaDtoNew);

        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Solo se aceptan valores F para femenino y M para masculino", jsonResult.Value);
    }



    // Email null
    [Fact]
    public async void Put_EmailNull_BadRequest()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);

        string id = "sd65sd6asd46asd4a6s5da6sd4a6s5da6";

        PersonaEditDTO personaDtoNew = new PersonaEditDTO()
        {
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = null,
            Genero = "M",
            Nacimiento = nacimiento,
            Telefono = "0985123450"
        };


        var result = await controller.Put(id, personaDtoNew);

        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("No se mando el email", jsonResult.Value);
    }



    // email person with id is no equal to email dto
    [Fact]
    public async void Put_DTONoEqualToModelWithIdRecieve_BadRequestk()
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


        PersonaEditDTO personaDtoNew = new PersonaEditDTO()
        {
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramon.ramirez@gmail.com",
            Genero = "M",
            Nacimiento = nacimiento,
            Telefono = "0985123450"
        };


        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "user@gmail.com")
            }, "role"));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        service.Setup(p => p.FindById(It.IsAny<string>())).ReturnsAsync(personaOld);

        service.Setup(p => p.ExistMail(It.IsAny<string>())).ReturnsAsync(true);


        var result = await controller.Put(id, personaDtoNew);


        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("El email nuevo que queres actualizar ya existe", jsonResult.Value);

    }



    // Exception FileNotFoundException 
    [Fact]
    public async void Put_FileNotFoundException_NotFound()
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
            Email = "ramon.ramirez@gmail.com",
            EmailConfirmed = true,
            Direccion = "Encarnacion",
            EsM = true,
            Nacimiento = nacimiento,
            Created = nacimiento,
            PhoneNumber = "0985123456"
        };


        PersonaEditDTO personaDtoNew = new PersonaEditDTO()
        {
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramon.ramirez@gmail.com",
            Genero = "M",
            Nacimiento = nacimiento,
            Telefono = "0985123450"
        };


        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "user@gmail.com")
            }, "role"));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        service.Setup(p => p.FindById(It.IsAny<string>())).ReturnsAsync(personaOld);

        service.Setup(p => p.ExistMail(It.IsAny<string>())).ReturnsAsync(true);

        service.Setup(p => p.EditProfile(It.IsAny<Persona>())).ThrowsAsync(new FileNotFoundException(""));

        var result = await controller.Put(id, personaDtoNew);


        var jsonResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("No se encontro el registro con el identificador indicado", jsonResult.Value);

    }



    // Exception BadHtttpRequestException
    [Fact]
    public async void Put_BadHtttpRequestExceptionForEmailExisting_BadRequestk()
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
            Email = "ramon.ramirez@gmail.com",
            EmailConfirmed = true,
            Direccion = "Encarnacion",
            EsM = true,
            Nacimiento = nacimiento,
            Created = nacimiento,
            PhoneNumber = "0985123456"
        };


        PersonaEditDTO personaDtoNew = new PersonaEditDTO()
        {
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramon.ramirez@gmail.com",
            Genero = "M",
            Nacimiento = nacimiento,
            Telefono = "0985123450"
        };


        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "user@gmail.com")
            }, "role"));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        service.Setup(p => p.FindById(It.IsAny<string>())).ReturnsAsync(personaOld);

        service.Setup(p => p.ExistMail(It.IsAny<string>())).ReturnsAsync(true);

        service.Setup(p => p.EditProfile(It.IsAny<Persona>())).ThrowsAsync(new BadHttpRequestException(""));

        var result = await controller.Put(id, personaDtoNew);


        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("El email que desea actualizar ya existe", jsonResult.Value);

    }


    // Generic exception
    [Fact]
    public async void Put_UnexpectedException_BadRequestk()
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
            Email = "ramon.ramirez@gmail.com",
            EmailConfirmed = true,
            Direccion = "Encarnacion",
            EsM = true,
            Nacimiento = nacimiento,
            Created = nacimiento,
            PhoneNumber = "0985123456"
        };


        PersonaEditDTO personaDtoNew = new PersonaEditDTO()
        {
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramon.ramirez@gmail.com",
            Genero = "M",
            Nacimiento = nacimiento,
            Telefono = "0985123450"
        };


        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "user@gmail.com")
            }, "role"));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        service.Setup(p => p.FindById(It.IsAny<string>())).ReturnsAsync(personaOld);

        service.Setup(p => p.ExistMail(It.IsAny<string>())).ReturnsAsync(true);

        service.Setup(p => p.EditProfile(It.IsAny<Persona>())).ThrowsAsync(new Exception(""));

        var result = await controller.Put(id, personaDtoNew);


        var jsonResult = Assert.IsType<ConflictResult>(result.Result);

    }

}