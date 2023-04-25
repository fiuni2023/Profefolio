using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Helpers;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Alumno;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Controllers;

[Authorize(Roles = "Administrador de Colegio")]
[Route("api/[controller]")]
public class AlumnosController : ControllerBase
{
    private readonly IPersona _personasService;
    private readonly IMapper _mapper;
    private readonly IRol _rolService;
    private readonly IColegiosAlumnos _colAlumnosService;
    private static int CantPerPage => Constantes.CANT_ITEMS_POR_PAGE;
    const string rol = "Alumno";

    public AlumnosController(IPersona personasService, IMapper mapper, IRol rolService, IColegiosAlumnos colAlumnosService)
    {
        _personasService = personasService;
        _mapper = mapper;
        _rolService = rolService;
        _colAlumnosService = colAlumnosService;
    }

    [HttpPost]
    public async Task<ActionResult<AlumnoGetDTO>> Post([FromBody] AlumnoCreateDTO dto)
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


        var userId = User.Identity.GetUserId();
        var entity = _mapper.Map<Persona>(dto);
        entity.Deleted = false;
        entity.CreatedBy = userId;
        var adminEmail = User.FindFirstValue(ClaimTypes.Name);

        try
        {
            var adminColegio = await _personasService.FindByEmail(adminEmail);

            // verificar que no exista el alumno
            var alumno = await _personasService.FindByDocumentoAndRole(dto.Documento, "Alumno");
            if (alumno != null)
            {
                // si ya existe el alumno creado
                return new CustomStatusResult<AlumnoGetDTO>(230, _mapper.Map<AlumnoGetDTO>(alumno));
            }

            // obtener el administrador de colegio


            var saved = await _personasService.CreateUser(entity, $"{dto.Email}.Mm123");

            if (await _rolService.AsignToUser("Alumno", saved)){
                // se agrega el alumno al colegio del administrador
                await _colAlumnosService.Add(new ColegiosAlumnos(){
                    ColegioId = adminColegio.Colegio.Id,
                    CreatedBy = adminEmail,
                    Created = DateTime.Now,
                    PersonaId = saved.Id
                });
                
                return Ok(_mapper.Map<AlumnoGetDTO>(saved));
            }
        }
        catch (BadHttpRequestException e)
        {
            Console.WriteLine(e.Message);
            return BadRequest(e.Message);
        }

        return BadRequest($"Error al crear al Usuario ${dto.Email}");

    }


    [Authorize(Roles = "Administrador de Colegio,Profesor")]
    [HttpGet]
    [Route("page/{page:int}")]
    public async Task<ActionResult<DataListDTO<AlumnoGetDTO>>> Get(int page)
    {

        var query = await _personasService
            .FilterByRol(page, CantPerPage, rol);

        var cantPages = (int)Math.Ceiling((double)await _personasService.CountByRol(rol) / CantPerPage);

        var result = new DataListDTO<AlumnoGetDTO>();

        if (page >= cantPages)
        {
            return BadRequest($"No existe la pagina: {page} ");
        }

        var enumerable = query as Persona[] ?? query.ToArray();
        result.CantItems = enumerable.Length;
        result.CurrentPage = page;
        result.Next = result.CurrentPage + 1 < cantPages;
        result.DataList = _mapper.Map<List<AlumnoGetDTO>>(enumerable.ToList());
        result.TotalPage = cantPages;
        return Ok(result);
    }
    [Authorize(Roles = "Administrador de Colegio,Profesor")]
    [HttpGet]
    [Route("id/{id}")]
    public async Task<ActionResult<AlumnoGetDTO>> Get(string id)
    {
        try
        {
            var persona = await _personasService.FindById(id);
            return Ok(_mapper.Map<AlumnoGetDTO>(persona));
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
        return await _personasService.DeleteByUserAndRole(id, rol) ? Ok() : NotFound();
    }


    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<AlumnoGetDTO>> Put(string id, [FromBody] AlumnoEditDTO dto)
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

        if (!id.Equals(dto.Id))
            return BadRequest("No valido");

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

            return Ok(_mapper.Map<AlumnoGetDTO>(query));
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


    private static void MapOldToNew(Persona persona, AlumnoEditDTO dto, string userId)
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
        persona.UserName = dto.Email;
        persona.NormalizedUserName = dto.Email.ToUpper();
        persona.NormalizedEmail = dto.Email.ToUpper();
    }


}