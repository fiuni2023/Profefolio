using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs.Persona;
using profefolio.Repository;

namespace profefolio.Controllers;

[Authorize(Roles = "Administrador de Colegio")]
[Route("[controller]")]
public class AlumnosController : ControllerBase
{
    private readonly IPersona _personaService;
    private readonly IMapper _mapper;

    public AlumnosController(IPersona personaService, IMapper mapper)
    {
        _personaService = personaService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<PersonaResultDTO>> Post([FromBody] PersonaDTO dto)
    {
        return Ok();
    }
}