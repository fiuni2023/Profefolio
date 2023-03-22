using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    private const int CantPerPage = 20;

    public AlumnosController(IPersona personasService, IMapper mapper, IRol rolService)
    {
        _personasService = personasService;
        _mapper = mapper;
        _rolService = rolService;
    }

        [HttpPost]
        public async Task<ActionResult<PersonaResultDTO>> Post([FromBody]AlumnoCreateDTO dto)
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

            try
            {
                var saved = await _personasService.CreateUser(entity, $"{dto.Email}.Mm123");

                if (await _rolService.AsignToUser("Alumno", saved))
                    return Ok(_mapper.Map<AlumnoGetDTO>(saved));
            }
            catch (BadHttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return BadRequest($"El email {dto.Email} ya existe");
            }
        
            return BadRequest($"Error al crear al Usuario ${dto.Email}");
        
        }
        [Authorize(Roles = "Administrador de Colegio,Profesor")]
        [HttpGet]
        [Route("page/{page:int}")]
        public async Task<ActionResult<DataListDTO<AlumnoGetDTO>>> Get(int page)
        {
            const string rol = "Alumno";
            var query = await _personasService
                .FilterByRol(page, CantPerPage, rol);

            var cantPages = (int) (await _personasService.CountByRol(rol) / CantPerPage)  + 1;

            var result = new DataListDTO<AlumnoGetDTO>();

            if(page >= cantPages) 
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
        


}