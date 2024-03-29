using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.DTOs.Colegio;
using profefolio.Models.Entities;
using profefolio.Repository;
using profefolio.Helpers;

/**
* Controlador que maneja al administrador solo por el id
* 
**/
namespace profefolio.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Master")]
    [ApiController]
    public class ColegiosController : ControllerBase
    {
        private readonly IColegio _colegioService;
        private static int _cantPorPag => Constantes.CANT_ITEMS_POR_PAGE;
        private readonly IMapper _mapper;
        public ColegiosController(IColegio colegioService, IMapper mapper)
        {
            _colegioService = colegioService;
            _mapper = mapper;
        }

        /**
        * Devuelve los datos del colegio con el id persona
        **/
        [HttpGet]
        [Route("page/{page}")]
        public ActionResult<DataListDTO<ColegioResultDTO>> GetColegios(int page)
        {
            var query = _colegioService.GetAll(page, _cantPorPag);
            int totalPage = (int)Math.Ceiling((double)_colegioService.Count() / _cantPorPag);

            var result = new DataListDTO<ColegioResultDTO>();

            var enumerable = query as Colegio[] ?? query.ToArray();
            result.CantItems = enumerable.Length;
            result.CurrentPage = page > totalPage ? totalPage : page;
            result.Next = result.CurrentPage + 1 < totalPage;
            result.DataList = _mapper.Map<List<ColegioResultDTO>>(enumerable.ToList());
            result.TotalPage = totalPage;

            return Ok(result);
        }

        // GET: api/Colegios/1
        //TODO: si data.delete = false no retornar.
        [HttpGet("{id}")]
        public async Task<ActionResult<ColegioResultDTO>> GetColegio(int id)
        {
            var colegio = await _colegioService.FindById(id);

            if (colegio == null)
            {

                return NotFound();
            }

            var response = _mapper.Map<ColegioResultDTO>(colegio);

            return Ok(response);
        }

        // PUT: api/Colegios/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // 
        // una solicitud PUT requiere que el cliente envíe toda la entidad actualizada, no solo los cambios.
        [HttpPut("{id}")]
        public async Task<ActionResult<ColegioResultDTO>> PutColegio(int id, ColegioDTO colegio)
        {
            //verificar el modelo
            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto No valido");
            }
            //verificar que PersonaId no sea nulo
            if (colegio.PersonaId == null)
            {
                return BadRequest("Administrador No valido");
            }
            //VERIFICAR REPETIDOS con nombre de colegio e id iguales
            var verificar = await _colegioService.FindByNamePerson(colegio.Nombre, colegio.PersonaId);
            if (verificar != null)
            {
                return BadRequest($"Ya existe el colegio.");
            }
            //VERIFICAR REPETIDOS con nombre de colegio igual
            var verificarNombreColegio = await _colegioService.ExistOtherWithEqualName(colegio.Nombre, id);
            if (verificarNombreColegio)
            {
                return BadRequest($"Ya existe un colegio con el mismo nombre.");
            }
            //verificar que no se repita PersonaId
            var persona = await _colegioService.FindByPerson(colegio.PersonaId);
            if (persona == null)
            {
                return BadRequest($"No existe el administrador.");
            }

            // verificar que el id del administrador nuevo no este asugnado a un colegio distinto al actual
            var existeAdmin = await _colegioService.ExistAdminInOtherColegio(colegio.PersonaId, id);
            if (existeAdmin)
            {
                return BadRequest("El administrador ya esta asignado a otro colegio.");
            }

            var p = await _colegioService.FindById(id);
            if (p == null)
            {
                return NotFound();
            }
            string userId = User.Identity.GetUserId();
            p.ModifiedBy = userId;
            p.Deleted = false;
            p.Modified = DateTime.Now;

            p.Nombre = colegio.Nombre;
            p.PersonaId = colegio.PersonaId;
            var query = _colegioService.Edit(p);
            await _colegioService.Save();

            //return Ok("Colegio: " + p.Nombre + ",PersonaId: " + p.PersonaId );
            return Ok(_mapper.Map<ColegioResultDTO>(query));
        }

        // POST: api/Colegios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ColegioWithAdminDataDTO>> PostColegio([FromBody] ColegioDTO colegio)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objeto no válido");
                }

                if (colegio.PersonaId == null)
                {
                    return BadRequest("Colegio no válido");
                }

                if (string.IsNullOrWhiteSpace(colegio.Nombre))
                {
                    return BadRequest("El nombre del colegio no puede estar vacío.");
                }

                var rol = await _colegioService.FindByPersonRol(colegio.PersonaId);
                if (rol == 0)
                {
                    return BadRequest("Persona no válida");
                }
                //verificar si el id ya fue asignado a un colegio
                //"El administrador ya esta asignado a un colegio."
                //buscar en la tabla colegios ese id administrador
                var verificarAdmin = await _colegioService.FindAdminColegio(colegio.PersonaId);
                if (verificarAdmin != null)
                {
                    return BadRequest($"El administrador ya esta asignado a un colegio.");
                }

                //si elimine un colegio y quiero volver a agregar con el mismo admin
                //actualizar el registro en lugar de agregar uno nuevo


                // Verificar duplicados con nombre de colegio e id iguales
                var verificar = await _colegioService.FindByNamePerson(colegio.Nombre, colegio.PersonaId);
                if (verificar != null)
                {
                    return BadRequest($"Ya existe el colegio y el ID de persona ingresado.");
                }

                // Verificar duplicados con nombre de colegio igual
                var verificarNombreColegio = await _colegioService.FindByNameColegio(colegio.Nombre);
                if (verificarNombreColegio != null)
                {
                    return BadRequest($"Ya existe un colegio con el mismo nombre.");
                }

                // Verificar ID
                var persona = await _colegioService.FindByPerson(colegio.PersonaId);
                if (persona == null)
                {
                    return BadRequest($"No existe el administrador.");
                }
                // Buscar el colegio eliminado por el ID del administrador
                var colegioEliminado = await _colegioService.FindAdminColegioDeleted(colegio.PersonaId);

                if (colegioEliminado != null)
                {
                    colegioEliminado.Deleted = false; // Marcar como no eliminado
                    colegioEliminado.Nombre = colegio.Nombre; // Actualizar el nombre si es necesario
                    var userId = User.Identity.GetUserId();
                    colegioEliminado.ModifiedBy = userId;
                    
                    await _colegioService.Save();

                    return Ok(_mapper.Map<ColegioWithAdminDataDTO>(colegioEliminado));
                }
                else
                {
                    var p = _mapper.Map<Colegio>(colegio);

                    var userId = User.Identity.GetUserId();
                    p.ModifiedBy = userId;
                    p.Deleted = false;

                    var saved = await _colegioService.Add(p);
                    await _colegioService.Save();

                    return Ok(_mapper.Map<ColegioWithAdminDataDTO>(saved));
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error al crear el colegio.");
            }
        }


        // DELETE: api/Colegios/1
        //TODO: estado = false al eliminar.
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _colegioService.FindById(id);

            if (data == null)
            {
                return NotFound();
            }

            data.Modified = DateTime.Now;
            data.Deleted = true;
            data.ModifiedBy = "Anonimous";
            _colegioService.Edit(data);
            await _colegioService.Save();

            return Ok();
        }

        [HttpGet("administradores/noAsignados")]
        public async Task<ActionResult<List<AdministradorDTO>>> GetAdministradoresNoAsignados()
        {
            try
            {
                var administradoresNoAsignados = await _colegioService.GetAdministradoresNoAsignados();
                var administradoresDTO = _mapper.Map<List<AdministradorDTO>>(administradoresNoAsignados);
                return Ok(administradoresDTO);
            }
            catch (Exception e)
            {
                return BadRequest("Error al obtener los administradores no asignados.");
            }
        }


    }
}