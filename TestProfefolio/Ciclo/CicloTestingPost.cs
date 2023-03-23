using System.Security.Claims;
using System.Security.Principal;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Primitives;
using NuGet.Protocol;
using profefolio.Controllers;
using profefolio.Models.DTOs.Ciclo;
using profefolio.Models.DTOs.Persona;
using profefolio.Repository;

namespace TestProfefolio.Ciclo;

public class CicloPostOk
{

    /*
        Testea cuando se guarda exitosamente el nuevo Ciclo
    */
    [Fact]
    public async void Post_Ok()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<ICiclo> service = new Mock<ICiclo>();
        CicloController controller = new CicloController(mapper.Object, service.Object);


        CicloDTO dto = new CicloDTO()
        {
            Nombre = "Primero"
        };

        profefolio.Models.Entities.Ciclo modelo = new profefolio.Models.Entities.Ciclo()
        {
            Id = 1,
            Nombre = "Primero",
            Created = DateTime.Now,
            Deleted = false,
            CreatedBy = "123456"
        };

        CicloResultDTO resultDto = new CicloResultDTO()
        {
            Id = 1,
            Nombre = "Primero"
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            //new Claim(ClaimTypes.NameIdentifier, "user1") -- usar esta linea si es que funciona el id del usuario autorizado
            new Claim(ClaimTypes.Name, "user1")
        }, "mock"));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        service.Setup(a => a.ExisitNombre(dto.Nombre)).ReturnsAsync(false);

        mapper.Setup(m => m.Map<profefolio.Models.Entities.Ciclo>(dto)).Returns(modelo);

        service.Setup(s => s.Add(modelo)).ReturnsAsync(modelo);

        service.Setup(a => a.Save()).Returns(Task.CompletedTask);

        mapper.Setup(m => m.Map<CicloResultDTO>(modelo)).Returns(resultDto);

        var result = await controller.Post(dto);

        Assert.IsType<OkObjectResult>(result.Result);
    }


    /*
        Testea cuando se pasa un modelo invalido al metodo de Post
    */
    [Fact]
    public async void Post_ModelInvalid_BadRequest()
    {

        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<ICiclo> service = new Mock<ICiclo>();
        CicloController controller = new CicloController(mapper.Object, service.Object);

        CicloDTO dto = new CicloDTO()
        {
            Nombre = "Primero"
        };

        profefolio.Models.Entities.Ciclo modelo = new profefolio.Models.Entities.Ciclo()
        {
            Id = 1,
            Nombre = "Primero",
            Created = DateTime.Now,
            Deleted = false,
            CreatedBy = "123456"
        };

        CicloResultDTO resultDto = new CicloResultDTO()
        {
            Id = 1,
            Nombre = "Primero"
        };


        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            //new Claim(ClaimTypes.NameIdentifier, "user1") -- usar esta linea si es que funciona el id del usuario autorizado
            new Claim(ClaimTypes.Name, "user1")
        }, "mock"));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        var modelState = new ModelStateDictionary();
        modelState.AddModelError("Nombre", "Propiedad invalida");
        controller.ModelState.Merge(modelState);

        var result = await controller.Post(dto);
        BadRequestObjectResult r = (BadRequestObjectResult)result.Result;


        Assert.Equal("Peticion invalido", r.Value.ToString());
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    /*
        Testea cuando se quiere agregar un nuevo Ciclo con un nombre ya existente
    */
    [Fact]
    public async void Post_ExistOtherWithEqualName_BadRequest()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<ICiclo> service = new Mock<ICiclo>();
        CicloController controller = new CicloController(mapper.Object, service.Object);


        CicloDTO dto = new CicloDTO()
        {
            Nombre = "Primero"
        };

        profefolio.Models.Entities.Ciclo modelo = new profefolio.Models.Entities.Ciclo()
        {
            Id = 1,
            Nombre = "Primero",
            Created = DateTime.Now,
            Deleted = false,
            CreatedBy = "123456"
        };

        CicloResultDTO resultDto = new CicloResultDTO()
        {
            Id = 1,
            Nombre = "Primero"
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "user1")
        }, "mock"));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        service.Setup(a => a.ExisitNombre(dto.Nombre)).ReturnsAsync(true);

        var result = await controller.Post(dto);
        BadRequestObjectResult r = (BadRequestObjectResult)result.Result;


        Assert.Equal("Ya existe un Ciclo con ese nombre", r.Value.ToString());
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }


    /*
        Testea cuando sucede un error durante el guardado del Ciclo
    */
    [Fact]
    public async void Post_ErrorSave_BadRequest()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<ICiclo> service = new Mock<ICiclo>();
        CicloController controller = new CicloController(mapper.Object, service.Object);


        CicloDTO dto = new CicloDTO()
        {
            Nombre = "Primero"
        };

        profefolio.Models.Entities.Ciclo modelo = new profefolio.Models.Entities.Ciclo()
        {
            Id = 1,
            Nombre = "Primero",
            Created = DateTime.Now,
            Deleted = false,
            CreatedBy = "123456"
        };

        CicloResultDTO resultDto = new CicloResultDTO()
        {
            Id = 1,
            Nombre = "Primero"
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "user1")
        }, "mock"));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        service.Setup(a => a.ExisitNombre(dto.Nombre)).ReturnsAsync(false);

        mapper.Setup(m => m.Map<profefolio.Models.Entities.Ciclo>(dto)).Returns(modelo);

        service.Setup(s => s.Add(modelo));//.ReturnsAsync(modelo);

        service.Setup(a => a.Save()).ThrowsAsync(new Exception("Error al guardar"));

        mapper.Setup(m => m.Map<CicloResultDTO>(modelo)).Returns(resultDto);

        var result = await controller.Post(dto);

        BadRequestObjectResult r = (BadRequestObjectResult)result.Result;

        Assert.Equal("Error durante el guardado.", r.Value.ToString());
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }
}