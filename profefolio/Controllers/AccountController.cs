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
[Authorize(Roles = "Master")]
public class AccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPersona _personasService;
    private readonly IRol _rolService;
    private const int CantPorPage = 20;


    public AccountController(IMapper mapper, IPersona personasService, IRol rolService)
    {
        _mapper = mapper;
        _personasService = personasService;
        _rolService = rolService;
       
    }
    
    [HttpPost]
    public async Task<ActionResult<PersonaResultDTO>> Post([FromBody]PersonaDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (dto.Password == null) return BadRequest("Password es requerido");
        var userId = User.Identity.GetUserId();
        var entity = _mapper.Map<Persona>(dto);
        entity.Deleted = false;
        entity.CreatedBy = userId;

        try
        {
            var saved = await _personasService.CreateUser(entity, dto.Password);

            if (await _rolService.AsignToUser("Administrador de Colegio", saved))
                return Ok(_mapper.Map<PersonaResultDTO>(saved));
        }
        catch (BadHttpRequestException e)
        {
            Console.WriteLine(e.Message);
            return BadRequest($"El email {dto.Email} ya existe");
        }
        
        return BadRequest($"Error al crear al Usuario ${dto.Email}");
        
    }

    [HttpGet]
    [Route("page/{page:int}")]
    public async Task<ActionResult<DataListDTO<PersonaResultDTO>>> Get(int page)
    {
        var query = await _personasService
            .FilterByRol(page, CantPorPage, "Administrador de Colegio");

        var cantPages = (int)Math.Ceiling((double)_personasService.Count() / CantPorPage);

        var result = new DataListDTO<PersonaResultDTO>();

        var enumerable = query as Persona[] ?? query.ToArray();
        result.CantItems = enumerable.Length;
        result.CurrentPage = page > cantPages ? cantPages : page;
        result.Next = result.CurrentPage + 1 < cantPages;
        result.DataList = _mapper.Map<List<PersonaResultDTO>>(enumerable.ToList());
        result.TotalPage = cantPages;

        return Ok(result);
    }

    [HttpGet]
    [Route("id/{id}")]
    public async Task<ActionResult<PersonaResultDTO>> Get(string id)
    {
        try
        {
            var persona = await _personasService.FindById(id);
            return Ok(_mapper.Map<PersonaResultDTO>(persona));
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);

        }

        return NotFound();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        return await _personasService.DeleteUser(id) ? Ok() : NotFound();
    }


    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<PersonaResultDTO>> Put(string id, [FromBody] PersonaDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (dto.Password == null) return BadRequest("Password es requerido");
        try
        {
            var userId = User.Identity.GetUserId();
            var personaOld = await _personasService.FindById(id);
            var personaNew = _mapper.Map<Persona>(dto);


            personaOld.Deleted = true;
            personaOld.Modified = DateTime.Now;
            personaOld.ModifiedBy = userId;
            personaOld.UserName = $"deleted.{personaOld.Id}.{personaOld.Email}";

            personaNew.Created = personaOld.Created;
            personaNew.CreatedBy = personaOld.CreatedBy;
            personaNew.Modified = DateTime.Now;
            personaNew.ModifiedBy = userId;
            var query = await _personasService.EditProfile(personaOld, personaNew, dto.Password);

            return Ok(_mapper.Map<PersonaResultDTO>(query));
        }
        catch (BadHttpRequestException e)
        {
            Console.WriteLine(e.Message);
            return BadRequest(e.Message);
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
            return NotFound();
        }

    }


}