using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using profefolio.Controllers;
using profefolio.Models.DTOs.Persona;
using profefolio.Repository;
namespace TestProfefolio.Persona;

public class PersonaGetDataTest
{
    private readonly  Mock<IPersona> _personaService = new Mock<IPersona>();
    private readonly  Mock<IMapper> _mapper = new Mock<IMapper>();
    private readonly PersonasController _personasController;
    private readonly PersonaDTO _personaDto;
    private readonly profefolio.Models.Entities.Persona _persona;
    
    public PersonaGetDataTest()
    {
        
        _personasController = new PersonasController(_personaService.Object, _mapper.Object);
        _persona = new profefolio.Models.Entities.Persona
        {
            Id = 1,
            Nombre = "Carlos",
            Apellido = "Torres",
            Deleted = false,
            Created = DateTime.Now,
            ModifiedBy = "",
            Edad = 21,
            Modified = DateTime.Now

        };
        
        _personaDto = new PersonaDTO
        {
            Id = 1,
            Nombre = "Carlos",
            Apellido = "Torres",
            Edad = 21
        };
    }

    [Fact]
    public async Task GetDataTest()
    {
        _personaService.Setup(p => p.FindById(_personaDto.Id))
            .ReturnsAsync(_persona);

        _mapper.Setup(m => m.Map<PersonaDTO>(_persona))
            .Returns(_personaDto);

        var result = _personasController
            .GetPersona(_personaDto.Id);
    }
}