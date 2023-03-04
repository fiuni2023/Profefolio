using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs;
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
    private const int CantPorPage = 20;
    private readonly string userId;


    public AccountController(IMapper mapper, IPersona personasService, IRol rolService)
    {
        _mapper = mapper;
        _personasService = personasService;
        _rolService = rolService;
        userId = User.Identity.GetUserId();
    }
    
    [HttpPost]
    public async Task<ActionResult<PersonaResultDTO>> Post([FromBody]PersonaDTO dto)
    {
        if (dto.Password == null) return BadRequest("Password requerido");
        var entity = _mapper.Map<Persona>(dto);
        entity.Deleted = false;
        entity.CreatedBy = userId;
        
        var saved = await _personasService.CreateUser(entity, dto.Password);
        if(await _rolService.AsignToUser("Administitrador de Colegio", saved))
            return Ok(_mapper.Map<PersonaResultDTO>(saved));
        
        return BadRequest($"Error al crear al Usuario ${dto.Email}");
    }

    [HttpGet]
    [Route("page/{page}")]
    public ActionResult Get(int page)
    {
        var query =  _personasService.GetAll(page, CantPorPage);

        var cantPages = (int)Math.Ceiling((double)_personasService.Count() / CantPorPage);

        var result = new DataListDTO<PersonaResultDTO>();

        var enumerable = query as Persona[] ?? query.ToArray();
        result.Items = enumerable.Length;
        result.CurrentPage = page > cantPages ? cantPages : page;
        result.Next = result.CurrentPage < cantPages;
        result.DataList = _mapper.Map<List<PersonaResultDTO>>(enumerable.ToList());
        result.TotalPage = cantPages;

        return Ok(result);
    }

    [HttpGet]
    [Route("id/{id}")]
    public async Task<ActionResult<PersonaResultDTO>> Get(string id)
    {
        var persona = await _personasService.FindById(id);
        return Ok(_mapper.Map<PersonaResultDTO>(persona));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        return await _personasService.DeleteUser(id) ? Ok() : NotFound();
    }


    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<PersonaResultDTO>> Put(int id, [FromBody] PersonaDTO dto)
    {
        var personaOld = await _personasService.FindById(id);
        var personaNew = _mapper.Map<Persona>(dto);
        
        personaOld.Deleted = true;
        personaOld.Modified =  DateTime.Now;
        personaOld.ModifiedBy = userId;

        personaNew.Created = personaOld.Created;
        personaNew.CreatedBy = personaOld.CreatedBy;
        personaNew.Modified = DateTime.Now;
        personaNew.ModifiedBy = userId;

        var query = await _personasService.EditProfile(personaNew, personaOld, dto.Password);

        return Ok(_mapper.Map<PersonaResultDTO>(query));
    }


}