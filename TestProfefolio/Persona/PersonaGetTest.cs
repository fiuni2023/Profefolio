using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using profefolio.Controllers;
using profefolio.Models.DTOs.Auth;
using profefolio.Models.DTOs.Persona;
using profefolio.Repository;
using profefolio.Models.Entities;



namespace TestProfefolio.Persona;

public class PersonaGetTest
{
    private readonly  Mock<IPersona> _personaService = new Mock<IPersona>();
    private readonly  Mock<IMapper> _mapper = new Mock<IMapper>();
    private readonly PersonasController _personasController;
    private readonly PersonaDTO _personaDto;
    private readonly profefolio.Models.Entities.Persona _persona;
    
    
    public PersonaGetTest()
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

        const string id = "aefaese123342";

        _personaService.Setup(s => s.FindById(It.IsAny<string>()))
            .ReturnsAsync(persona);

        _mapper.Setup(m => m.Map<PersonaDTO>(_persona))
            .Returns(_personaDto);

        var result = await _personasController.GetPersona(_personaDto.Id);

        Assert.IsType<OkObjectResult>(result.Result);
        
    }

}