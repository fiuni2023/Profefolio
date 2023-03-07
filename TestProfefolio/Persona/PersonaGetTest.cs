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

    
    
    public PersonaGetTest()
    {
       
    }
    
    
    [Fact]
    public async Task TestGetPersonaHttpStatusOk()
    {
      
        
    }

    [Fact]
    public async Task TestGetPersonaHttpStatusNotFound()
    {
       
    }


   
}