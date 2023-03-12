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

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public async void Get_Page_Ok(int page)
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();
        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);

        List<PersonaResultDTO> profesoresDtos = new List<PersonaResultDTO>()
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

        int cantPages = (int)Math.Ceiling((page == 0 ? (double)profesores.Count() : 0) / 20);
        var dataList = new DataListDTO<PersonaResultDTO>()
        {
            DataList = profesoresDtos,
            TotalPage = cantPages,
            Next = false,
            CurrentPage = page > cantPages ? cantPages : page,
            CantItems = page == 0 ? profesoresDtos.Count() : 0
        };


        service.Setup(p => p.GetAllByRol("Profesor", page, 20))
            .ReturnsAsync(page == 0 ? profesores : new List<profefolio.Models.Entities.Persona>());

        mapper.Setup(a => a.Map<List<PersonaResultDTO>>(profesores.ToList())).Returns(profesoresDtos);

        ActionResult<DataListDTO<PersonaResultDTO>> result = await controller.Get(page);

        //Assert.IsType<OkObjectResult>(result.Result);
        DataListDTO<PersonaResultDTO> rdto = (DataListDTO<PersonaResultDTO>)(((OkObjectResult)result.Result).Value);

        Assert.Equal<PersonaResultDTO>(page == 0 ? dataList.DataList : null, rdto.DataList);
        Assert.Equal<int>(page == 0 ? dataList.CantItems : 0, rdto.CantItems);
        Assert.Equal<int>(dataList.CurrentPage, rdto.CurrentPage);
        Assert.Equal<int>(dataList.TotalPage, rdto.TotalPage);
        Assert.Equal<bool>(dataList.Next, rdto.Next);
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

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);
        

        service.Setup(a => a.FindById(id)).Throws(new FileNotFoundException());
        
        var result = await controller.Get(id);

        Assert.IsType<NotFoundObjectResult>(result.Result);
        
    }
    
    [Theory]
    [InlineData("")]
    public async void GetById_LengthCero(string id)
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

        //service.Setup(a => a.FindById(id)).Throws(new FileNotFoundException());
        
        var result = await controller.Get(id);

        Assert.IsType<BadRequestObjectResult>(result.Result);
        
    }
}