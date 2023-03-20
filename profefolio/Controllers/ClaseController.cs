using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Clase;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Controllers
{
    [ApiController]
    //[Authorize(Roles = "Administrador de Colegio,Profesor")]
    [Route("api/[controller]")]
    public class ClaseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClase _claseService;
        private readonly ICiclo _cicloService;
        private readonly IColegio _colegioService;
        private const int CantPorPage = 20;

        public ClaseController(IMapper mapper, IClase claseService, ICiclo cicloService, IColegio colegioService)
        {
            _mapper = mapper;
            _claseService = claseService;
            _cicloService = cicloService;
            _colegioService = colegioService;
        }

        [HttpGet("byColegio/{idColegio:int}")]
        public async Task<ActionResult<IEnumerable<ClaseResultSimpleDTO>>> GetAllByColegioId(int idColegio)
        {
            if (idColegio < 0)
            {
                return NotFound();
            }
            try
            {

                var result = await _claseService.GetByIdColegio(idColegio);

                return Ok(_mapper.Map<List<ClaseResultSimpleDTO>>(result));

            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                return BadRequest("Error durante la busqueda");

            }
        }

        [HttpGet("page/{idColegio:int}/{page:int}")]
        public async Task<ActionResult<DataListDTO<ClaseResultSimpleDTO>>> GetAll(int idColegio, int page)
        {
            if (page < 0)
            {
                return BadRequest("El numero de pagina debe ser mayor o igual que cero");
            }
            if(idColegio < 0){
                return BadRequest("El colegio es invalido");
            }

            var clases = await _claseService.GetAllByIdColegio(page, CantPorPage, idColegio);


            int cantPages = (int)(_claseService.Count()  / CantPorPage) + 1;


            var result = new DataListDTO<ClaseResultSimpleDTO>();

            if (page >= cantPages)
            {
                return BadRequest($"No existe la pagina: {page} ");
            }

            result.CantItems = clases.Count();
            result.CurrentPage = page;
            result.Next = result.CurrentPage + 1 < cantPages;
            result.DataList = _mapper.Map<List<ClaseResultSimpleDTO>>(clases.ToList());
            result.TotalPage = cantPages;

            return Ok(result);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClaseResultDTO>> GetById(int id)
        {
            try
            {
                var result = await _claseService.FindById(id);

                return result != null 
                    ? Ok(_mapper.Map<ClaseResultDTO>(result)) 
                    : NotFound($"No se encontro la Clase con id: {id}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Error durante la busqueda");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClaseResultDTO>> Post([FromBody] ClaseDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto invalido");
            }

            try
            {
                var clase = _mapper.Map<Clase>(dto);

                var ciclo = await _cicloService.FindById(dto.CicloId);
                if (ciclo == null)
                {
                    return BadRequest("El ciclo no existe");
                }

                var colegio = await _colegioService.FindById(dto.ColegioId);
                if (colegio == null)
                {
                    return BadRequest("El colegio no existe");
                }

                if (dto.Anho <= 0)
                {
                    return BadRequest("Anho invalido");
                }


                var userId = User.Identity.GetUserId();
                clase.CreatedBy = userId;
                clase.Created = DateTime.Now;
                clase.Deleted = false;

                var result = await _claseService.Add(clase);

                await _claseService.Save();

                return Ok(_mapper.Map<ClaseResultDTO>(result));
            }
            catch (Exception e)
            {
                _claseService.Dispose();
                Console.WriteLine(e);
                return BadRequest("Error durante el guardado.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest("Id invalido");
            }

            try
            {
                var clase = await _claseService.FindById(id);
                if (clase == null)
                {
                    return NotFound();
                }

                string userId = User.Identity.GetUserId();
                clase.ModifiedBy = userId;
                clase.Modified = DateTime.Now;
                clase.Deleted = true;

                _claseService.Edit(clase);
                await _claseService.Save();

                return Ok();

            }
            catch (Exception e)
            {
                _claseService.Dispose();
                Console.WriteLine(e);
                return BadRequest("Error durante la eliminacion");
            }
        }

    }
}