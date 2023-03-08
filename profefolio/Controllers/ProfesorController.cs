using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.Entities;
using profefolio.Repository;
using Microsoft.AspNet.Identity;

namespace profefolio.Controllers
{
    [ApiController]
    [Authorize(Roles = "Administrador de Colegio")]
    [Route("api/[controller]")]
    public class ProfesorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPersona _personasService;
        private readonly IRol _rolService;
        private const int CantPorPage = 20;

        private const string PROFESOR_ROLE = "Profesor";


        public ProfesorController(IMapper mapper, IPersona personasService, IRol rolService)
        {
            _mapper = mapper;
            _personasService = personasService;
            _rolService = rolService;
        }

        /*public ProfesorController()
        {
            
        }*/

        [HttpGet("page/{page:int}")]
        public async Task<ActionResult<IEnumerable<PersonaResultDTO>>> Get(int page)
        {
            var profesores = await _personasService.GetAllByRol(PROFESOR_ROLE, page, CantPorPage);

            if (profesores == null)
            {
                return BadRequest("");
            }
            int cantPages = (int)Math.Ceiling((double)profesores.Count() / CantPorPage);

            var result = new DataListDTO<PersonaResultDTO>();

            var enumerable = profesores as Persona[] ?? profesores.ToArray();
            result.CantItems = enumerable.Length;
            result.CurrentPage = page > cantPages ? cantPages : page;
            result.Next = result.CurrentPage + 1 < cantPages;
            result.DataList = _mapper.Map<List<PersonaResultDTO>>(enumerable.ToList());
            result.TotalPage = cantPages;

            return Ok(result);


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfesorDTO>> Get(string id)
        {
            if (id != null && id.Length > 0)
            {
                var profesor = await _personasService.FindById(id);
                if (profesor != null)
                {
                    return Ok(_mapper.Map<ProfesorDTO>(profesor));
                }
                else
                {
                    return NotFound("No se encontro al profesor");
                }
            }
            else
            {
                return (BadRequestObjectResult)BadRequest("ID invalido");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PersonaResultDTO>> Post([FromBody] PersonaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            else if (dto.Password == null)
            {
                return BadRequest("Falta el Password");
            }
            else if (dto.ConfirmPassword == null)
            {
                return BadRequest("Falta confirmacion de Password");
            }


            var userId = User.Identity.GetUserId();
            var entity = _mapper.Map<Persona>(dto);
            entity.Deleted = false;
            entity.CreatedBy = userId;
            try
            {
                var saved = await _personasService.CreateUser(entity, dto.Password);

                if (await _rolService.AsignToUser(PROFESOR_ROLE, saved))
                {
                    return Ok(_mapper.Map<PersonaResultDTO>(saved));
                }
            }
            catch (BadHttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return BadRequest($"El email {dto.Email} ya existe");
            }

            return BadRequest($"Error al crear al Usuario ${dto.Email}");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PersonaResultDTO>> Put(string id, [FromBody] PersonaDTO dto)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                var personaOld = await _personasService.FindById(id);
                var personaNew = _mapper.Map<Persona>(dto);


                personaOld.Deleted = true;
                personaOld.Modified = DateTime.Now;
                personaOld.ModifiedBy = userId;

                personaNew.Created = personaOld.Created;
                personaNew.CreatedBy = personaOld.CreatedBy;
                personaNew.Modified = DateTime.Now;
                personaNew.ModifiedBy = userId;

                try
                {
                    var query = await _personasService.EditProfile(personaOld, personaNew, dto.Password);

                    return Ok(_mapper.Map<PersonaResultDTO>(query));
                }
                catch (BadHttpRequestException e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest(e.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    


    [HttpDelete("{id}")]
    public async Task<ActionResult<PersonaResultDTO>> Delete(string id)
    {
        return await _personasService.DeleteUser(id) ? Ok() : NotFound();
    }
}
}