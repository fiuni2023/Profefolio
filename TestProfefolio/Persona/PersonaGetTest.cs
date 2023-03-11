using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using profefolio.Controllers;
using profefolio.Models.DTOs.Auth;
using profefolio.Models.DTOs.Persona;
using profefolio.Repository;



namespace TestProfefolio.Persona;

public class PersonaGetTest
{
    private readonly Mock<IPersona> _personaService = new Mock<IPersona>();
    private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
    private readonly Mock<IRol> _rolService = new Mock<IRol>();
    private readonly AccountController _accountController;


    public PersonaGetTest()
    {
        _accountController = new AccountController(_mapper.Object, _personaService.Object, _rolService.Object);
    }
    
    
    [Fact]
    public async Task TestGetPersonaHttpStatusOk()
    {
        var persona = new profefolio.Models.Entities.Persona()
        {
            Nombre = "Carlos",
            Apellido = "Torres",
            Nacimiento = new DateTime(1999, 06, 10),
            Direccion = "Avda 123",
            DocumentoTipo = "CI",
            Documento = "1214311",
            PhoneNumber = "+234 234 333",
            EsM = true,
            Id = "aefaese123342"
        };

        var personaResultDto = new PersonaResultDTO()
        {
            Nombre = "Carlos",
            Apellido = "Torres",
            Nacimiento = new DateTime(1999, 06, 10),
            Direccion = "Avda 123",
            DocumentoTipo = "CI",
            Documento = "1214311",
            Telefono = "+234 234 333",
            Genero = "Masculino",
            Id = "aefaese123342"
        };

        const string id = "aefaese123342";

        _personaService.Setup(s => s.FindById(It.IsAny<string>()))
            .ReturnsAsync(persona);

        _mapper.Setup(m => m.Map<PersonaResultDTO>(persona))
            .Returns(personaResultDto);

        var result = await _accountController.Get(id);

        Assert.IsType<OkObjectResult>(result.Result);

    }

}