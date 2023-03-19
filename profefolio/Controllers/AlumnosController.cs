using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Controllers;

[Authorize(Roles = "Administrador de Colegio")]
[Route("[controller]")]
public class AlumnosController : ControllerBase
{
    private readonly IPersona _personasService;
    private readonly IMapper _mapper;
    private readonly IRol _rolService;

        public AlumnosController(IPersona personasService, IMapper mapper, IRol rolService)
    {
        _personasService = personasService;
        _mapper = mapper;
        _rolService = rolService;
    }

    [HttpPost]
    public async Task<ActionResult<PersonaResultDTO>> Post([FromBody] PersonaDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.Identity.GetUserId();
        var alumno = _mapper.Map<Persona>(dto);
        alumno.CreatedBy = userId;
        
        try
        {
            var saved = await _personasService.CreateUser(alumno, dto.Email);

            if (await _rolService.AsignToUser("Alummno", saved))
                return Ok(_mapper.Map<PersonaResultDTO>(saved));
        }
        catch (BadHttpRequestException e)
        {
            Console.WriteLine(e.Message);
            return BadRequest($"El email {dto.Email} ya existe");
        }
        return BadRequest();
    }
}