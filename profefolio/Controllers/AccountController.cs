using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.Entities;
using profefolio.Repository;


namespace profefolio.Controllers;

[Route("api/administrador")]
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

    [Route("create")]
    [HttpPost]
    public async Task<ActionResult<PersonaDTO>> CrearAdminstrador([FromBody]PersonaDTO dto)
    {
        var entity = _mapper.Map<Persona>(dto);
        entity.Deleted = false;
        entity.CreatedBy = User.Identity.GetUserId();
        
        var saved = await _personasService.CreateUser(entity, dto.Password);
        if(await _rolService.AsignToUser("Administrador de Colegio", saved))
            return Ok(_mapper.Map<PersonaResultDTO>(saved));
        
        return BadRequest($"Error al crear al Usuario ${dto.Email}");
    }

}