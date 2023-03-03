using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.Entities;
using profefolio.Repository;


namespace profefolio.Controllers;

[Route("api/[controller]")]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPersona _personasService;
    private readonly IRol _rolService;


    public AccountController(IMapper mapper, IPersona personasService, IRol rolService)
    {
        _mapper = mapper;
        _personasService = personasService;
        _rolService = rolService;
    }

    [HttpPost]
    public async Task<ActionResult<PersonaDTO>> CrearAdminstrador(PersonaDTO dto)
    {
        if (dto.Password == null)
            return BadRequest("Falta el Password");
        if (!ModelState.IsValid)
        {
            return BadRequest("");
        }
        string mailLogged = await _personasService.UserLogged();

        var entity = _mapper.Map<Persona>(dto);
        entity.Deleted = false;
        entity.CreatedBy = mailLogged;
        
        var saved = await _personasService.CreateUser(entity, dto.Password);
        if(await _rolService.AsignToUser("Administrador de Colegio", saved))
            return Ok(_mapper.Map<PersonaResultDTO>(saved));
        return BadRequest($"Error al crear al Usuario ${dto.Email}");
    }

}