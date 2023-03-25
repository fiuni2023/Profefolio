using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Auth;
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
    private const int CantPorPage = 5;
    private const string ROL_ADMIN = "Administrador de Colegio";

    private const int CantPorPage = 20;



    public AccountController(IMapper mapper, IPersona personasService, IRol rolService)
    {
        _mapper = mapper;
        _personasService = personasService;
        _rolService = rolService;

    }

    [HttpPost]
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

        if (dto.Genero == null)
        {
            return BadRequest();
        }

        if (!(dto.Genero.Equals("M") || dto.Genero.Equals("F")))
        {
            return BadRequest("Solo se aceptan valores F para femenino y M para masculino");
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
        const string rol = "Administrador de Colegio";
        var query = await _personasService
            .FilterByRol(page, CantPorPage, rol);


        var cantPages = (int)Math.Ceiling((double) await _personasService.CountByRol(rol)/ CantPorPage);


        var result = new DataListDTO<PersonaResultDTO>();

        if (page >= cantPages)
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

    [HttpGet]
    public async Task<ActionResult<List<PersonaSimpleDTO>>> GetAll()
    {
        try
        {
            var personas = await _personasService.GetAllByRol(ROL_ADMIN);
            return Ok(_mapper.Map<List<PersonaSimpleDTO>>(personas));
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