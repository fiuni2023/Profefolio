using System.Collections;
using System.Collections.ObjectModel;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using profefolio.Repository;
using profefolio.Controllers;
using profefolio.Helpers.Mappers;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Persona;
using profefolio.Services;
using profefolio.Models.Entities;

namespace TestProfefolio.Profesor;

public class ProfesorTesting
{
    private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
    private readonly Mock<IPersona> _service = new Mock<IPersona>();
    private readonly Mock<IRol> _rol = new Mock<IRol>();

    private readonly DateTime nacimiento = DateTime.Now;

    private ProfesorController _controller { get; set; }

    public ProfesorTesting()
    {
        _controller = new ProfesorController(_mapper.Object, _service.Object, _rol.Object);
    }

    [Fact]
    public async void GetPage0_Ok()
    {
        int page = 0;
        var result = await _controller.Get(0);
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Theory]
    [InlineData("123456")]
    public async void getByID_Ok(string id)
    {
        profefolio.Models.Entities.Persona _persona = new profefolio.Models.Entities.Persona
        {
            Id = "123456",
            Nombre = "Carlos",
            Apellido = "Torres",
            Deleted = false,
            Created = DateTime.Now,
            ModifiedBy = "",
            Modified = DateTime.Now
        };
        ProfesorDTO _dto = new ProfesorDTO
        {
            Id = "123456",
            Nombre = "Torres",
        };

        _service.Setup(a => a.FindById(id)).ReturnsAsync(_persona);

        _mapper.Setup(m => m.Map<ProfesorDTO>(_persona))
            .Returns(_dto);

        var result = await _controller.Get(_persona.Id);

        Assert.IsType<OkObjectResult>(result.Result);
    }


    [Theory]
    [InlineData("TeSt CaSe")]
    [InlineData("Tasdasds")]
    public async void GetById_NotFound(string id)
    {
        profefolio.Models.Entities.Persona _persona = new profefolio.Models.Entities.Persona
        {
            Id = "123456",
            Nombre = "Carlos",
            Apellido = "Torres",
            Deleted = false,
            Created = DateTime.Now,
            ModifiedBy = "",
            Modified = DateTime.Now
        };
        ProfesorDTO _dto = new ProfesorDTO
        {
            Id = "123456",
            Nombre = "Torres",
        };

        _service.Setup(a => a.FindById(id)).ReturnsAsync(_persona);
        

        var result = await _controller.Get(_persona.Id);

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async void Post_Ok()
    {
        profefolio.Models.Entities.Persona _persona = new profefolio.Models.Entities.Persona
        {
            Id = "133",
            Nombre = "Carlos",
            Apellido = "Torres",
            Deleted = false,
            Created = DateTime.Now,
            ModifiedBy = "",
            Modified = DateTime.Now
            
        };

        PersonaDTO _personaDto = new PersonaDTO()
        {
            Nombre = "Abel",
            Apellido = "Trinidad",
            Direccion = "Encarnacion",
            Documento = "5484898",
            DocumentoTipo = "CI",
            Email = "abeltrinidad@gmail.com",
            Genero = "Masculino",
            Nacimiento = nacimiento,
            Telefono = "0985464456",
            Password = "12345678",
            ConfirmPassword = "12345678",
        };
        PersonaResultDTO _dto = new PersonaResultDTO()
        {
            Id = "133",
            Nombre = "Abel",
            Apellido = "Trinidad",
            Direccion = "Encarnacion",
            Documento = "5484898",
            DocumentoTipo = "CI",
            Genero = "Masculino",
            Nacimiento = nacimiento,
            Telefono = "0985464456"
        };
        
        _mapper.Setup(m => m.Map<profefolio.Models.Entities.Persona>(_personaDto)).Returns(_persona);


        _service.Setup(b => b.CreateUser(_persona, _personaDto.Password)).ReturnsAsync(_persona);

        _rol.Setup(a => a.AsignToUser("Profesor", _persona)).ReturnsAsync(true);
        _mapper.Setup(m => m.Map<PersonaResultDTO>(_persona)).Returns(_dto);
        var result = await _controller.Post(_personaDto);
        
        Assert.IsType<OkObjectResult>(result.Result);
    }
    
    [Fact]
    public async void Post_Failed_PasswordNull()
    {
        profefolio.Models.Entities.Persona _persona = new profefolio.Models.Entities.Persona
        {
            Id = "133",
            Nombre = "Abel",
            Apellido = "Trinidad",
            Deleted = false,
            Created = DateTime.Now,
            ModifiedBy = "",
            Modified = DateTime.Now,
            Email = "abeltrinidad@gmail.com"
        };

        PersonaDTO _personaDto = new PersonaDTO()
        {
            Nombre = "",
            Apellido = "Trinidad",
            Direccion = "Encarnacion",
            Documento = "5484898",
            DocumentoTipo = "CI",
            Email = "abeltrinidad@gmail.com",
            Genero = "Masculino",
            Nacimiento = nacimiento,
            Telefono = "0985464456",
            Password = null,
            ConfirmPassword = null,
        };
        PersonaResultDTO _dto = new PersonaResultDTO()
        {
            Id = "133",
            Nombre = "Abel",
            Apellido = "Trinidad",
            Direccion = "Encarnacion",
            Documento = "5484898",
            DocumentoTipo = "CI",
            Genero = "Masculino",
            Nacimiento = nacimiento,
            Telefono = "0985464456"
        };
        
        _mapper.Setup(m => m.Map<profefolio.Models.Entities.Persona>(_personaDto)).Returns(_persona);
       
        _service.Setup(b => b.CreateUser(_persona, _personaDto.Password)).ReturnsAsync(_persona);

        _rol.Setup(a => a.AsignToUser("Profesor", _persona)).ReturnsAsync(true);
        _mapper.Setup(m => m.Map<PersonaResultDTO>(_persona)).Returns(_dto);

        var result = await _controller.Post(_personaDto);
        BadRequestObjectResult r = (BadRequestObjectResult)result.Result;
        Assert.Equal("Falta el Password", r.Value.ToString());
       
    }
    
    [Fact]
    public async void Post_Failed_ConfirmPasswordNull()
    {
        profefolio.Models.Entities.Persona _persona = new profefolio.Models.Entities.Persona
        {
            Id = "133",
            Nombre = "Carlos",
            Apellido = "Torres",
            Deleted = false,
            Created = DateTime.Now,
            ModifiedBy = "",
            Modified = DateTime.Now
            
        };

        PersonaDTO _personaDto = new PersonaDTO()
        {
            Nombre = "Abel",
            Apellido = "Trinidad",
            Direccion = "Encarnacion",
            Documento = "5484898",
            DocumentoTipo = "CI",
            Email = "abeltrinidad@gmail.com",
            Genero = "Masculino",
            Nacimiento = nacimiento,
            Telefono = "0985464456",
            Password = "12345678",
            ConfirmPassword = null,
        };
        PersonaResultDTO _dto = new PersonaResultDTO()
        {
            Id = "133",
            Nombre = "Abel",
            Apellido = "Trinidad",
            Direccion = "Encarnacion",
            Documento = "5484898",
            DocumentoTipo = "CI",
            Genero = "Masculino",
            Nacimiento = nacimiento,
            Telefono = "0985464456"
        };
        
        _mapper.Setup(m => m.Map<profefolio.Models.Entities.Persona>(_personaDto)).Returns(_persona);
        
        _service.Setup(b => b.CreateUser(_persona, _personaDto.Password)).ReturnsAsync(_persona);

        _rol.Setup(a => a.AsignToUser("Profesor", _persona)).ReturnsAsync(true);
        _mapper.Setup(m => m.Map<PersonaResultDTO>(_persona)).Returns(_dto);

        var result = await _controller.Post(_personaDto);
        BadRequestObjectResult r = (BadRequestObjectResult)result.Result;
        Assert.Equal("Falta confirmacion de Password", r.Value.ToString());
    }


   /*  [Fact]
    public async void Put_Ok()
    {
        string id = "123456";
        profefolio.Models.Entities.Persona _persona = new profefolio.Models.Entities.Persona
        {
            Nombre = "Abel",
            Apellido = "Trinidad",
            Direccion = "Encarnacion",
            Documento = "5484898",
            DocumentoTipo = "CI",
            Email = "abeltrinidad@gmail.com",
            Nacimiento = nacimiento,
            Deleted = false,
            Created = DateTime.Now,
            ModifiedBy = "",
            Modified = DateTime.Now
            
        };
        PersonaDTO _personaDto = new PersonaDTO()
        {
            Nombre = "Abel",
            Apellido = "Trinidad",
            Direccion = "Encarnacion",
            Documento = "5484898",
            DocumentoTipo = "CI",
            Email = "abeltrinidad@gmail.com",
            Genero = "Masculino",
            Nacimiento = nacimiento,
            Telefono = "0985464456",
            Password = "12345678",
            ConfirmPassword = "12345678"
        };

        _mapper.Setup(m => m.Map<profefolio.Models.Entities.Persona>(_personaDto)).Returns(_persona);
        _service.Setup(s => s.EditProfile(id, _persona)).ReturnsAsync(_persona);

        var result = await _controller.PutProfesor(id, _personaDto);

        Assert.IsType<NoContentResult>(result);
    } */
    
    /* [Fact]
    public async void Put_Failed_Id_NotFound()
    {
        string id = "123456";
        profefolio.Models.Entities.Persona _persona = new profefolio.Models.Entities.Persona
        {
            Nombre = "Abel",
            Apellido = "Trinidad",
            Direccion = "Encarnacion",
            Documento = "5484898",
            DocumentoTipo = "CI",
            Email = "abeltrinidad@gmail.com",
            Nacimiento = nacimiento,
            Deleted = false,
            Created = DateTime.Now,
            ModifiedBy = "",
            Modified = DateTime.Now
            
        };
        PersonaDTO _personaDto = new PersonaDTO()
        {
            Nombre = "Abel",
            Apellido = "Trinidad",
            Direccion = "Encarnacion",
            Documento = "5484898",
            DocumentoTipo = "CI",
            Email = "abeltrinidad@gmail.com",
            Genero = "Masculino",
            Nacimiento = nacimiento,
            Telefono = "0985464456",
            Password = "12345678",
            ConfirmPassword = "12345678"
        };
        IQueryable<profefolio.Models.Entities.Persona> q = Queryable.AsQueryable(new List<profefolio.Models.Entities.Persona>());


        Mock<UserManager<profefolio.Models.Entities.Persona>> _manager = new Mock<UserManager<profefolio.Models.Entities.Persona>>();



        _manager.Setup(m => m.Users);
        _manager.Setup(m => m.UpdateAsync(_persona));
        
        _mapper.Setup(m => m.Map<profefolio.Models.Entities.Persona>(_personaDto)).Returns(_persona);


        _service.Setup(s => s.EditProfile(id, _persona));

        var result = await _controller.PutProfesor(id, _personaDto);

        Assert.IsType<NotFoundObjectResult>(result);
    } */
    
/*     [Fact]
    public async void Put_Failed_Profesor_NotFound()
    {
        string id = "123456";
        profefolio.Models.Entities.Persona _persona = new profefolio.Models.Entities.Persona
        {
            Nombre = "Abel",
            Apellido = "Trinidad",
            Direccion = "Encarnacion",
            Documento = "5484898",
            DocumentoTipo = "CI",
            Email = "abeltrinidad@gmail.com",
            Nacimiento = nacimiento,
            Deleted = false,
            Created = DateTime.Now,
            ModifiedBy = "",
            Modified = DateTime.Now
            
        };
        PersonaDTO _personaDto = new PersonaDTO()
        {
            Nombre = "Abel",
            Apellido = "Trinidad",
            Direccion = "Encarnacion",
            Documento = "5484898",
            DocumentoTipo = "CI",
            Email = "abeltrinidad@gmail.com",
            Genero = "Masculino",
            Nacimiento = nacimiento,
            Telefono = "0985464456",
            Password = "12345678",
            ConfirmPassword = "12345678"
        };
        IQueryable<profefolio.Models.Entities.Persona> q = Queryable.AsQueryable(new List<profefolio.Models.Entities.Persona>());


        Mock<UserManager<profefolio.Models.Entities.Persona>> _manager = new Mock<UserManager<profefolio.Models.Entities.Persona>>();



        _manager.Setup(m => m.Users);
        _manager.Setup(m => m.UpdateAsync(_persona));
        
        _mapper.Setup(m => m.Map<profefolio.Models.Entities.Persona>(_personaDto)).Returns(_persona);

        
        _service.Setup(s => s.EditProfile(id, _persona)).Throws(new FileNotFoundException());
        

        var result = await _controller.Put(id, _personaDto);

        BadRequestObjectResult r = ((BadRequestObjectResult)result.Result);

        Assert.Equal("No se encontro el registro a editar", r.Value.ToString());

        //Assert.IsType<BadRequestObjectResult>(result);
    } */

/*     [Fact]
    public async void Put_Failed_Map()
    {
        string id = "123456";
        profefolio.Models.Entities.Persona _persona = new profefolio.Models.Entities.Persona
        {
            Nombre = "Abel",
            Apellido = "Trinidad",
            Direccion = "Encarnacion",
            Documento = "5484898",
            DocumentoTipo = "CI",
            Email = "abeltrinidad@gmail.com",
            Nacimiento = nacimiento,
            Deleted = false,
            Created = DateTime.Now,
            ModifiedBy = "",
            Modified = DateTime.Now
            
        };
        PersonaDTO _personaDto = new PersonaDTO()
        {
            Nombre = "Abel",
            Apellido = "Trinidad",
            Direccion = "Encarnacion",
            Documento = "5484898",
            DocumentoTipo = "CI",
            Email = "abeltrinidad@gmail.com",
            Genero = "Masculino",
            Nacimiento = nacimiento,
            Telefono = "0985464456",
            Password = "12345678",
            ConfirmPassword = "12345678"
        };
        IQueryable<profefolio.Models.Entities.Persona> q = Queryable.AsQueryable(new List<profefolio.Models.Entities.Persona>());


        Mock<UserManager<profefolio.Models.Entities.Persona>> _manager = new Mock<UserManager<profefolio.Models.Entities.Persona>>();



        _manager.Setup(m => m.Users);
        _manager.Setup(m => m.UpdateAsync(_persona));
        
        _mapper.Setup(m => m.Map<profefolio.Models.Entities.Persona>(_personaDto));

        
        _service.Setup(s => s.EditProfile(id, _persona)).ReturnsAsync(_persona);
        

        var result = await _controller.Put(id, _personaDto);

        BadRequestObjectResult r = ((BadRequestObjectResult)result.Result);

        Assert.Equal("Error al tratar de crear", r.Value.ToString());

        
    } */
}