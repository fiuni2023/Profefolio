using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using profefolio.Controllers;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace TestProfefolio.Profesor;

public class ProfesorTestingPosts
{
    private static readonly DateTime nacimiento = DateTime.Now;

    [Fact]
    public async void Post_Ok()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        Mock<IColegioProfesor> serviceColProf = new Mock<IColegioProfesor>();
        Mock<IProfesor> serviceProfesor = new Mock<IProfesor>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object, serviceColProf.Object, serviceProfesor.Object);

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
            PhoneNumber = "0985123456"
        };

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
            Password = "12345678",
            ConfirmPassword = "12345678"
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
            Telefono = "0985123456"
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "user@gmail.com")
            }, "role"));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mapper.Setup(m => m.Map<profefolio.Models.Entities.Persona>(It.IsAny<PersonaDTO>())).Returns(persona);


        service.Setup(b => b.CreateUser(It.IsAny<Persona>(), It.IsAny<string>())).ReturnsAsync(persona);

        rol.Setup(a => a.AsignToUser(It.IsAny<string>(), It.IsAny<Persona>())).ReturnsAsync(true);
        mapper.Setup(m => m.Map<PersonaResultDTO>(It.IsAny<Persona>())).Returns(dtoResult);


        var result = await controller.Post(personaDto);

        var jsonResult = Assert.IsType<OkObjectResult>(result.Result);

        var objResult = Assert.IsType<PersonaResultDTO>(jsonResult.Value);

        Assert.Equal(dtoResult.Id, objResult.Id);
        Assert.Equal(dtoResult.Nombre, objResult.Nombre);
        Assert.Equal(dtoResult.Apellido, objResult.Apellido);
        Assert.Equal(dtoResult.Direccion, objResult.Direccion);
        Assert.Equal(dtoResult.Email, objResult.Email);
        Assert.Equal(dtoResult.Documento, objResult.Documento);
        Assert.Equal(dtoResult.DocumentoTipo, objResult.DocumentoTipo);
        Assert.Equal(dtoResult.Telefono, objResult.Telefono);
        Assert.Equal(dtoResult.Nacimiento, objResult.Nacimiento);
        Assert.Equal(dtoResult.Genero, objResult.Genero);

    }


    [Fact]
    public async void Post_BadRequest_PasswordNull()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        Mock<IColegioProfesor> serviceColProf = new Mock<IColegioProfesor>();
        Mock<IProfesor> serviceProfesor = new Mock<IProfesor>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object, serviceColProf.Object, serviceProfesor.Object);


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

        var result = await controller.Post(personaDto);

        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Falta el Password", jsonResult.Value);
    }


    [Fact]
    public async void Post_BadRequest_ConfirmPasswordNull()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        Mock<IColegioProfesor> serviceColProf = new Mock<IColegioProfesor>();
        Mock<IProfesor> serviceProfesor = new Mock<IProfesor>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object, serviceColProf.Object, serviceProfesor.Object);

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
            Password = "12345678",
            ConfirmPassword = null
        };

        var result = await controller.Post(personaDto);

        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);

        Assert.Equal("Falta confirmacion de Password", jsonResult.Value);
    }



    [Fact]
    public async void Post_ModelInvalid_BadRequest()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        Mock<IColegioProfesor> serviceColProf = new Mock<IColegioProfesor>();
        Mock<IProfesor> serviceProfesor = new Mock<IProfesor>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object, serviceColProf.Object, serviceProfesor.Object);
        controller.ModelState.AddModelError("model_error", "El modelo es erroneo");
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
            Password = "12345678",
            ConfirmPassword = null
        };

        var result = await controller.Post(personaDto);

        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);

    }


    [Fact]
    public async void Post_InvalidDateOfBirth_BadRequest()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        Mock<IColegioProfesor> serviceColProf = new Mock<IColegioProfesor>();
        Mock<IProfesor> serviceProfesor = new Mock<IProfesor>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object, serviceColProf.Object, serviceProfesor.Object);

        PersonaDTO personaDto = new PersonaDTO()
        {
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramonramirez@gmail.com",
            Genero = "M",
            Nacimiento = nacimiento.AddDays(3),
            Telefono = "0985123456",
            Password = "12345678",
            ConfirmPassword = "123456789"
        };

        var result = await controller.Post(personaDto);

        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("El nacimiento no puede ser mayor a la fecha de hoy", jsonResult.Value);
    }


    [Fact]
    public async void Post_GenderNull_BadRequest()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        Mock<IColegioProfesor> serviceColProf = new Mock<IColegioProfesor>();
        Mock<IProfesor> serviceProfesor = new Mock<IProfesor>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object, serviceColProf.Object, serviceProfesor.Object);

        PersonaDTO personaDto = new PersonaDTO()
        {
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramonramirez@gmail.com",
            Genero = null,
            Nacimiento = nacimiento,
            Telefono = "0985123456",
            Password = "12345678",
            ConfirmPassword = "123456789"
        };

        var result = await controller.Post(personaDto);

        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("El genero no puede ser nulo", jsonResult.Value);
    }



    [Fact]
    public async void Post_InvalidGender_BadRequest()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        Mock<IColegioProfesor> serviceColProf = new Mock<IColegioProfesor>();
        Mock<IProfesor> serviceProfesor = new Mock<IProfesor>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object, serviceColProf.Object, serviceProfesor.Object);

        PersonaDTO personaDto = new PersonaDTO()
        {
            Nombre = "Ramon",
            Apellido = "Ramirez",
            Direccion = "Encarnacion",
            Documento = "7894689",
            DocumentoTipo = "CI",
            Email = "ramonramirez@gmail.com",
            Genero = "X",
            Nacimiento = nacimiento,
            Telefono = "0985123456",
            Password = "12345678",
            ConfirmPassword = "123456789"
        };

        var result = await controller.Post(personaDto);

        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Solo se aceptan valores F para femenino y M para masculino", jsonResult.Value);
    }



    [Fact]
    public async void Post_EmailExist_BadRequest()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        Mock<IColegioProfesor> serviceColProf = new Mock<IColegioProfesor>();
        Mock<IProfesor> serviceProfesor = new Mock<IProfesor>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object, serviceColProf.Object, serviceProfesor.Object);

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
            PhoneNumber = "0985123456"
        };

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
            Password = "12345678",
            ConfirmPassword = "12345678"
        };


        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "user@gmail.com")
            }, "role"));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mapper.Setup(m => m.Map<profefolio.Models.Entities.Persona>(It.IsAny<PersonaDTO>())).Returns(persona);


        service.Setup(b => b.CreateUser(It.IsAny<Persona>(), It.IsAny<string>())).ThrowsAsync(new BadHttpRequestException("El email al cual quiere registrarse ya existe"));

        var result = await controller.Post(personaDto);

        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);

        Assert.Equal("El email al cual quiere registrarse ya existe", jsonResult.Value);

    }


    [Fact]
    public async void Post_DocumentNumberExist_BadRequest()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        Mock<IColegioProfesor> serviceColProf = new Mock<IColegioProfesor>();
        Mock<IProfesor> serviceProfesor = new Mock<IProfesor>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object, serviceColProf.Object, serviceProfesor.Object);

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
            PhoneNumber = "0985123456",
            Colegio = new Colegio()
            {
                Id = 1,
                Nombre = "San Juan",
                PersonaId = "adsadasdads"
            }
        };

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
            Password = "12345678",
            ConfirmPassword = "12345678"
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "user@gmail.com")
            }, "role"));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mapper.Setup(m => m.Map<profefolio.Models.Entities.Persona>(It.IsAny<PersonaDTO>())).Returns(persona);

        service.Setup(a => a.ExistMail(It.IsAny<string>())).ReturnsAsync(false);

        service.Setup(a => a.ExistDoc(It.IsAny<Persona>())).ReturnsAsync(false);

        service.Setup(a => a.FindByEmail(It.IsAny<string>())).ReturnsAsync(persona);

        serviceProfesor.Setup(b => b.Add(It.IsAny<Persona>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ThrowsAsync(new BadHttpRequestException($"El usuario con doc {persona.Documento} ya existe"));

        var result = await controller.Post(personaDto);

        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);

        Assert.Equal($"El usuario con doc {persona.Documento} ya existe", jsonResult.Value);

    }


    [Fact]
    public async void Post_InvalidPasswordFormat_BadRequest()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        Mock<IColegioProfesor> serviceColProf = new Mock<IColegioProfesor>();
        Mock<IProfesor> serviceProfesor = new Mock<IProfesor>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object, serviceColProf.Object, serviceProfesor.Object);

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
            PhoneNumber = "0985123456",
            Colegio = new Colegio()
            {
                Id = 1,
                Nombre = "San Juan",
                PersonaId = "adsadasdads"
            }
        };

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
            Password = "12345678",
            ConfirmPassword = "12345678"
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "user@gmail.com")
            }, "role"));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mapper.Setup(m => m.Map<profefolio.Models.Entities.Persona>(It.IsAny<PersonaDTO>())).Returns(persona);


        service.Setup(a => a.ExistMail(It.IsAny<string>())).ReturnsAsync(false);

        service.Setup(a => a.ExistDoc(It.IsAny<Persona>())).ReturnsAsync(false);

        service.Setup(a => a.FindByEmail(It.IsAny<string>())).ReturnsAsync(persona);

        serviceProfesor.Setup(b => b.Add(It.IsAny<Persona>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ThrowsAsync(new InvalidOperationException("Formato invalido de constraseña. Debe contener mayusculas, minusculas, numeros y caracteres."));

        var result = await controller.Post(personaDto);

        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);

        Assert.Equal("Formato invalido de constraseña. Debe contener mayusculas, minusculas, numeros y caracteres.", jsonResult.Value);

    }


    [Fact]
    public async void Post_AddFailed_BadRequest()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        Mock<IColegioProfesor> serviceColProf = new Mock<IColegioProfesor>();
        Mock<IProfesor> serviceProfesor = new Mock<IProfesor>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object, serviceColProf.Object, serviceProfesor.Object);

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
            PhoneNumber = "0985123456"
        };

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
            Password = "12345678",
            ConfirmPassword = "12345678"
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "user@gmail.com")
            }, "role"));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mapper.Setup(m => m.Map<profefolio.Models.Entities.Persona>(It.IsAny<PersonaDTO>())).Returns(persona);


        service.Setup(b => b.CreateUser(It.IsAny<Persona>(), It.IsAny<string>())).ThrowsAsync(new Exception());

        var result = await controller.Post(personaDto);

        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);

        Assert.Equal("Error durante el guardado", jsonResult.Value);

    }



    [Fact]
    public async void Post_AsignRoleFalse_BadRequest()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        Mock<IColegioProfesor> serviceColProf = new Mock<IColegioProfesor>();
        Mock<IProfesor> serviceProfesor = new Mock<IProfesor>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object, serviceColProf.Object, serviceProfesor.Object);

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
            PhoneNumber = "0985123456"
        };

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
            Password = "12345678",
            ConfirmPassword = "12345678"
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "user@gmail.com")
            }, "role"));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mapper.Setup(m => m.Map<profefolio.Models.Entities.Persona>(It.IsAny<PersonaDTO>())).Returns(persona);


        service.Setup(b => b.CreateUser(It.IsAny<Persona>(), It.IsAny<string>())).ReturnsAsync(persona);

        rol.Setup(b => b.AsignToUser(It.IsAny<string>(), It.IsAny<Persona>())).ReturnsAsync(false);

        var result = await controller.Post(personaDto);

        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);

        Assert.Equal($"Error al crear al Usuario ${persona.Email}", jsonResult.Value);

    }

}
