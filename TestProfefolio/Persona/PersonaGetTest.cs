using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using profefolio.Controllers;
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
       
    }
    
    
    [Fact]
    public async Task TestGetPersonaHttpStatusOk()
    {
        _personaService.Setup(p => p.FindById(_persona.Id))
            .ReturnsAsync(_persona);

        _mapper.Setup(m => m.Map<PersonaDTO>(_persona))
            .Returns(_personaDto);

        var result = await _personasController.GetPersona(_personaDto.Id);

        Assert.IsType<OkObjectResult>(result.Result);
        
    }

    [Fact]
    public async Task TestGetPersonaHttpStatusNotFound()
    {
        _personaService.Setup(p => p.FindById(2))
            .ReturnsAsync((profefolio.Models.Entities.Persona)null);

        

        var result = await _personasController.GetPersona(_personaDto.Id);

        Assert.IsType<NotFoundResult>(result.Result);
    }


   
}