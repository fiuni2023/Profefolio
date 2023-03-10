using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using profefolio.Controllers;
using profefolio.Models.DTOs.Persona;
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
        
        mapper.Setup(m => m.Map<profefolio.Models.Entities.Persona>(personaDto)).Returns(persona);

        
        
        service.Setup(b => b.CreateUser(persona, personaDto.Password)).ReturnsAsync(persona);

        rol.Setup(a => a.AsignToUser("Profesor", persona)).ReturnsAsync(true);
        mapper.Setup(m => m.Map<PersonaResultDTO>(persona)).Returns(dtoResult);

        
        var result = await controller.Post(personaDto);
        
        Assert.IsType<OkObjectResult>(result.Result);
    }
}