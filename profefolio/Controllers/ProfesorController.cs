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
    [Authorize(Roles = "Administrador de Colegio,Profesor")]
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


        [HttpGet("page/{page:int}")]
        public async Task<ActionResult<DataListDTO<PersonaResultDTO>>> Get(int page)
        {
            var profesores = await _personasService.GetAllByRol(PROFESOR_ROLE, page, CantPorPage);


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
        public async Task<ActionResult<PersonaResultDTO>> Get(string id)
        {
            if (id.Length > 0)
            {
                try
                {
                    var profesor = await _personasService.FindById(id);
                    return Ok(_mapper.Map<PersonaResultDTO>(profesor));
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine((e.Message));
                    return NotFound("No se encontro al profesor");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest("Error inesperado");
                }
            }
            else
            {
                return BadRequest("ID invalido");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PersonaResultDTO>> Post([FromBody] PersonaDTO dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dto.Password == null)
            {
                return BadRequest("Falta el Password");
            }

            if (dto.ConfirmPassword == null)
            {
                return BadRequest("Falta confirmacion de Password");
            }


            var userId = User.Identity.GetUserId();
            var entity = _mapper.Map<Persona>(dto);

            entity.Deleted = false;
            entity.CreatedBy = userId;
            //Para que el username sea unico
            /* DateTime now = DateTime.Now;
            entity.UserName = $"{entity.Email}.{now}";
            entity.NormalizedUserName = $"{entity.Email.ToUpper()}.{now}"; */

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
            catch(InvalidOperationException e){
                Console.WriteLine(e.Message);
                return BadRequest("Formato invalido de constraseña. Debe contener mayusculas, minusculas, numeros y caracteres.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }

            return BadRequest($"Error al crear al Usuario ${dto.Email}");
        }

        /*
        [HttpPut("{id}")]
        public async Task<ActionResult<PersonaResultDTO>> Put(string id, [FromBody] PersonaDTO dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (dto.Password == null) return BadRequest("Password es requerido");

                    var userId = User.Identity.GetUserId();
                    var personaOld = await _personasService.FindById(id);
                    var personaNew = _mapper.Map<Persona>(dto);

                    personaOld.Deleted = true;
                    personaOld.Modified = DateTime.Now;
                    personaOld.ModifiedBy = userId;
                    personaOld.Email = $"deleted.{personaOld.Email}.{personaOld.Id}";

                    personaNew.Created = personaOld.Created;
                    personaNew.CreatedBy = personaOld.CreatedBy;
                    personaNew.Modified = DateTime.Now;
                    personaNew.ModifiedBy = userId;

                    //Para que el username sea unico
                    var now = DateTime.Now.ToOADate();
                    personaNew.UserName = $"{personaNew.Email}.{now}";
                    personaNew.NormalizedUserName = $"{personaNew.Email.ToUpper()}.{now}";

                    var query = await _personasService.EditProfile(personaOld, personaNew, dto.Password);

                    return Ok(_mapper.Map<PersonaResultDTO>(query));
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    return NotFound("No se encontro el registro con el identificador indicado");
                }
                catch (BadHttpRequestException e)
                {
                    _personasService.Dispose();
                    Console.WriteLine(e.Message);
                    return BadRequest("El email que desea actualizar ya existe");
                }
                catch (Exception e)
                {
                    _personasService.Dispose();
                    Console.WriteLine(e);
                    return Conflict("Error al tratar de editar el usuario");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
*/

        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonaResultDTO>> Delete(string id)
        {
            return await _personasService.DeleteUser(id) ? Ok() : NotFound();
        }
    }
}