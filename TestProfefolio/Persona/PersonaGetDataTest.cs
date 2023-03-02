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
        
       
    }

    [Fact]
    public async Task GetDataTest()
    {
        
    }
}