using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Auth;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.DTOs.Colegio;
using profefolio.Models.Entities;
using profefolio.Repository;
using profefolio.Helpers;

namespace profefolio.Controllers;

[Route("api/administrador")]
public class AccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPersona _personasService;
    private readonly IRol _rolService;
    private readonly IColegio _colegioService;
    private const string RolAdmin = "Administrador de Colegio";
    private static int CantPerPage => Constantes.CANT_ITEMS_POR_PAGE;



    public AccountController(IMapper mapper, IPersona personasService, IRol rolService, IColegio colegioService)
    {
        _mapper = mapper;
        _personasService = personasService;
        _rolService = rolService;
        _colegioService = colegioService;
    }

    [HttpPost]
    [Authorize(Roles = "Master")]
    public async Task<ActionResult<PersonaResultDTO>> Post([FromBody] PersonaDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (dto.Nacimiento > DateTime.Now)
        {
            return BadRequest("El nacimiento no puede ser mayor a la fecha de hoy");
        }

        if (!(dto.Genero.Equals("M") || dto.Genero.Equals("F")))
        {
            return BadRequest("Solo se aceptan valores F para femenino y M para masculino");
        }

        var userId = User.Identity.Name;

        

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
            return BadRequest(e.Message);
        }

        return BadRequest($"Error al crear al Usuario ${dto.Email}");

    }

    [HttpGet]
    [Authorize(Roles = "Master")]

    [Route("page/{page:int}")]
    public async Task<ActionResult<DataListDTO<PersonaResultDTO>>> Get(int page)
    {

        var query = await _personasService
            .FilterByRol(page, CantPerPage, RolAdmin);


        var cantPages = (int)Math.Ceiling((double)await _personasService.CountByRol(RolAdmin) / CantPerPage);


        var result = new DataListDTO<PersonaResultDTO>();

        if (page >= cantPages || page < 0)
        {
            return BadRequest($"No existe la pagina: {page} ");
        }

        var enumerable = query as Persona[] ?? query.ToArray();
        result.CantItems = enumerable.Length;
        result.CurrentPage = page;
        result.Next = result.CurrentPage + 1 < cantPages;
        result.DataList = _mapper.Map<List<PersonaResultDTO>>(enumerable.ToList());
        result.TotalPage = cantPages;

        return Ok(result);
    }

    [HttpGet]
    [Route("id/{id}")]
    [Authorize(Roles = "Master")]
    public async Task<ActionResult<PersonaResultDTO>> Get(string id)
    {
        try
        {
            var persona = await _personasService.FindByIdAndRole(id, RolAdmin);
            return Ok(_mapper.Map<PersonaResultDTO>(persona));
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);

        }

        return NotFound();
    }

    [HttpGet]
    [Authorize(Roles = "Master")]
    public async Task<ActionResult<List<PersonaSimpleDTO>>> GetAll()
    {
        try
        {
            var personas = await _personasService.GetAllByRol(RolAdmin);
            return Ok(_mapper.Map<List<PersonaSimpleDTO>>(personas));
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }

        return NotFound();
    }

    [HttpDelete]
    [Authorize(Roles = "Master")]
    [Route("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        return await _personasService.DeleteByUserAndRole(id, RolAdmin) ? Ok() : NotFound();
    }


    [HttpPut]
    [Authorize(Roles = "Master")]
    [Route("{id}")]
    public async Task<ActionResult<PersonaResultDTO>> Put(string id, [FromBody] PersonaEditDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (dto.Nacimiento > DateTime.Now)
        {
            return BadRequest("El nacimiento no puede ser mayor a la fecha de hoy");
        }

        if (!(dto.Genero.Equals("M") || dto.Genero.Equals("F")))
        {
            return BadRequest("Solo se aceptan valores F para femenino y M para masculino");
        }

        try
        {
            var persona = await _personasService.FindById(id);

            var userId = User.Identity.GetUserId();

            var existMail = await _personasService.ExistMail(dto.Email);

            var isEqual = dto.Email.Equals(persona.Email);

            if (!isEqual && existMail)
            {
                return BadRequest($"Ya existe el email '{dto.Email}', intente con otro");
            }

            MapOldToNew(persona, dto, userId);

            if ((!persona.Email.Equals(dto.Email)) && await _personasService.ExistMail(dto.Email))
            {
                return BadRequest("El email nuevo que queres actualizar ya existe");
            }



            var query = await _personasService.EditProfile(persona);

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

    [HttpPut]
    [Authorize(Roles = "Master")]
    [Route("change/password/{id}")]
    public async Task<ActionResult> ChangePassword(string id, [FromBody] ChangePasswordDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var personaOld = await _personasService.FindById(id);

            Console.WriteLine(personaOld.Id);

            if (await _personasService.ChangePassword(personaOld, dto.Password))
            {
                return Ok();
            }

            return BadRequest("No se pudo actualizar");
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
            return NotFound();
        }
    }


    [HttpGet("{email}")]
    [Authorize(Roles = "Administrador de Colegio")]
    public async Task<ActionResult<ColegioSimpleDTO>> GetColegioByAdminEmail(string email)
    {
        if (email == null)
        {
            return BadRequest("El email es invalido");
        }
        var emailToken = User.FindFirstValue(ClaimTypes.Name);
        
        if(!email.Equals(email)){
            return BadRequest("El email recibido no coincide con el de su autorizacion.");
        }

        try
        {
            Persona persona = await _personasService.FindByEmail(email);
            if(persona == null){
                return NotFound("El email no fue encontrado.");
            }

            Colegio colegio = await _colegioService.FindByIdAdmin(persona.Id);
            if(colegio == null){
                return NotFound("El usuario no fue asignado a ningun colegio todavia.");
            }
            return Ok(new ColegioSimpleDTO(){
                Id = colegio.Id,
                Nombre = colegio.Nombre
            });
        }   
        catch (Exception e)
        {
            Console.WriteLine(e);

            return BadRequest("Error durante la busqueda");
        }
    }

    private void MapOldToNew(Persona persona, PersonaEditDTO dto, string userId)
    {
        persona.Nombre = dto.Nombre;
        persona.Apellido = dto.Apellido;
        persona.Email = dto.Email;
        persona.EsM = dto.Genero.Equals("M");
        persona.Nacimiento = dto.Nacimiento;
        persona.Documento = dto.Documento;
        persona.Direccion = dto.Direccion;
        persona.Modified = DateTime.Now;
        persona.DocumentoTipo = dto.DocumentoTipo;
        persona.ModifiedBy = userId;
        persona.PhoneNumber = dto.Telefono;
        persona.UserName = dto.Email;
        persona.NormalizedUserName = dto.Email.ToUpper();
        persona.NormalizedEmail = dto.Email.ToUpper();
    }

}