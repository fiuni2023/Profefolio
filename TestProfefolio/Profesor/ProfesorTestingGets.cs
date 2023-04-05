using System.Collections;
using System.Collections.ObjectModel;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using profefolio.Repository;
using profefolio.Controllers;
using profefolio.Helpers.Mappers;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Persona;
using profefolio.Services;
using profefolio.Models.Entities;

namespace TestProfefolio.Profesor;

public class ProfesorTestingGets
{
    private static readonly DateTime nacimiento = DateTime.Now;

    private IEnumerable<profefolio.Models.Entities.Persona> profesores = new List<profefolio.Models.Entities.Persona>()
    {
        new profefolio.Models.Entities.Persona()
        {
            Id = "sd65sd6asd46asd4a6s5da6sd4a6s5d46",
            UserName = "JuanPerez",
            Nombre = "Juan",
            Apellido = "Perez",
            Documento = "7894612",
            DocumentoTipo = "CI",
            Email = "juanperez@gmail.com",
            EmailConfirmed = true,
            Direccion = "Encarnacion",
            EsM = true,
            Nacimiento = nacimiento,
            Created = nacimiento,
            CreatedBy = "ramonramirez@gmail.com",
            PhoneNumber = "0985123456"
        },
        new profefolio.Models.Entities.Persona()
        {
            Id = "sd65sd6asd46asd4a6s5da6sd4a6s5d47",
            UserName = "JuanPiris",
            Nombre = "Juan",
            Apellido = "Piris",
            Documento = "7894613",
            DocumentoTipo = "CI",
            Email = "juanpiris@gmail.com",
            EmailConfirmed = true,
            Direccion = "Encarnacion",
            EsM = true,
            Nacimiento = nacimiento,
            Created = nacimiento,
            CreatedBy = "ramonramirez@gmail.com",
            PhoneNumber = "0985123456"
        },
        new profefolio.Models.Entities.Persona()
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
        }
    };

    private List<PersonaResultDTO> profesoresDtos = new List<PersonaResultDTO>()
        {
            new PersonaResultDTO()
            {
                Id = "sd65sd6asd46asd4a6s5da6sd4a6s5d46",
                Nombre = "Juan",
                Apellido = "Perez",
                Documento = "7894612",
                DocumentoTipo = "CI",
                Direccion = "Encarnacion",
                Nacimiento = nacimiento,
                Genero = "M",
                Telefono = "0985123456"
            },
            new PersonaResultDTO()
            {
                Id = "sd65sd6asd46asd4a6s5da6sd4a6s5d47",
                Nombre = "Juan",
                Apellido = "Piris",
                Documento = "7894613",
                DocumentoTipo = "CI",
                Direccion = "Encarnacion",
                Nacimiento = nacimiento,
                Genero = "M",
                Telefono = "0985123456"
            },
            new PersonaResultDTO()
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
            }
        };

    [Theory]
    [InlineData(0)]
    public async void Get_Page_Ok(int page)
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);


        int cantPages = (int)Math.Ceiling((double)profesores.Count() / 20);
        var dataList = new DataListDTO<PersonaResultDTO>()
        {
            DataList = profesoresDtos,
            TotalPage = cantPages,
            Next = false,
            CurrentPage = page > cantPages ? cantPages : page,
            CantItems = profesoresDtos.ToList().Count
        };


        service.Setup(a => a.FilterByRol(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(profesores.AsEnumerable());

        service.Setup(a => a.CountByRol(It.IsAny<string>())).ReturnsAsync(profesores.Count());

        service.Setup(p => p.GetAllByRol(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(profesores);


        mapper.Setup(a => a.Map<List<PersonaResultDTO>>(It.IsAny<List<Persona>>())).Returns(profesoresDtos);


        var result = await controller.Get(page);



        var jsonResult = Assert.IsType<OkObjectResult>(result.Result);

        var datalistResult = Assert.IsType<DataListDTO<PersonaResultDTO>>(jsonResult.Value);

        Assert.Equal<PersonaResultDTO>(dataList.DataList, datalistResult.DataList);
        Assert.Equal<int>(dataList.CantItems, datalistResult.CantItems);
        Assert.Equal<int>(dataList.CurrentPage, datalistResult.CurrentPage);
        Assert.Equal<int>(dataList.TotalPage, datalistResult.TotalPage);
        Assert.Equal<bool>(dataList.Next, datalistResult.Next);
    }



    [Theory]
    [InlineData(-1)]
    public async void GetPage_PageLessThatZero_BadRequest(int page)
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);




        var result = await controller.Get(page);



        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("El numero de pagina debe ser mayor o igual que cero", jsonResult.Value);

    }



    [Theory]
    [InlineData(1)]
    public async void GetPage_PageNoExist_BadRequest(int page)
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);


        int cantPages = (int)Math.Ceiling((double)profesores.Count() / 20);
        var dataList = new DataListDTO<PersonaResultDTO>()
        {
            DataList = profesoresDtos,
            TotalPage = cantPages,
            Next = false,
            CurrentPage = page > cantPages ? cantPages : page,
            CantItems = profesoresDtos.ToList().Count
        };


        service.Setup(a => a.FilterByRol(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(profesores.AsEnumerable());

        service.Setup(a => a.CountByRol(It.IsAny<string>())).ReturnsAsync(profesores.Count());

        service.Setup(p => p.GetAllByRol(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(profesores);



        var result = await controller.Get(page);



        var jsonResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal($"No existe la pagina: {page}", jsonResult.Value);
    }












    [Theory]
    [InlineData("sd65sd6asd46asd4a6s5da6sd4a6s5da6")]
    public async void GetByID_Ok(string id)
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);

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

        service.Setup(a => a.FindById(It.IsAny<string>())).ReturnsAsync(persona);

        mapper.Setup(m => m.Map<PersonaResultDTO>(It.IsAny<Persona>())).Returns(dto);

        var result = await controller.Get(id);

        var jsonResult = Assert.IsType<OkObjectResult>(result.Result);
        var profResult = Assert.IsType<PersonaResultDTO>(jsonResult.Value);
        Assert.Equal(dto.Id, profResult.Id);
        Assert.Equal(dto.Nombre, profResult.Nombre);
        Assert.Equal(dto.Apellido, profResult.Apellido);
        Assert.Equal(dto.Direccion, profResult.Direccion);
        Assert.Equal(dto.Documento, profResult.Documento);
        Assert.Equal(dto.DocumentoTipo, profResult.DocumentoTipo);
        Assert.Equal(dto.Email, profResult.Email);
        Assert.Equal(dto.Genero, profResult.Genero);
        Assert.Equal(dto.Nacimiento, profResult.Nacimiento);
    }


    [Theory]
    [InlineData("Tasdasds")]
    public async void GetById_NotFound(string id)
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);


        service.Setup(a => a.FindById(It.IsAny<string>())).Throws(new FileNotFoundException());

        var result = await controller.Get(id);

        var msg = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("No se encontro al profesor", msg.Value);
    }

    [Theory]
    [InlineData("")]
    public async void GetById_LengthCero(string id)
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);

        var result = await controller.Get(id);

        var msg = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("ID invalido", msg.Value);

    }
}
