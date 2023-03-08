using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.DTOs.Colegio;
using profefolio.Models.Entities;
using profefolio.Repository;

/**
* Controlador que maneja al administrador solo por el id
* 
**/
namespace profefolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColegiosController : ControllerBase
    {
        private readonly IColegio _colegioService;
        private readonly int _cantPorPag = 10;
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
        public ActionResult<DataListDTO<ColegioDTO>> GetColegios(int page)
        {
            var query = _colegioService.GetAll();
            int totalPage = (int)Math.Ceiling((double)_colegioService.Count() / _cantPorPag);
            var result = query
            .Skip(_cantPorPag * page)
            .Take(_cantPorPag);

            return Ok(new DataListDTO<ColegioDTO>()
            {
                TotalPage = totalPage,
                CurrentPage = page,
                Items = result.Count(),
                Next = page < totalPage,
                DataList = _mapper.Map<List<ColegioDTO>>(result.ToList())
            });
        }

        // GET: api/Colegios/1
        //TODO: si data.delete = false no retornar.
        [HttpGet("{id}")]
        public async Task<ActionResult<ColegioDTO>> GetColegio(int id)
        {
            var colegio = await _colegioService.FindById(id);
            Console.Write("Colegio: ", colegio);
            if (colegio == null)
            {
                Console.Write("Colegio == null");
                return NotFound();
            }
            
            var response = _mapper.Map<ColegioDTO>(colegio);

            return Ok(response);
        }

        // PUT: api/Colegios/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // CUANDO SE edita SE CAMBIA SU ID ORIGINAL A ID+1
        // una solicitud PUT requiere que el cliente envíe toda la entidad actualizada, no solo los cambios.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColegio(int id, ColegioDTO colegio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != colegio.Id)
            {
                return BadRequest("Los ID no se actualizan");
            }
            var p = await _colegioService.FindById(id);
            if (p == null)
            {
                return NotFound();
            }
            p.ModifiedBy = "Anonimous";
            p.Deleted = true;
            p.Modified = DateTime.Now;
            
            var newColegio = _mapper.Map<Colegio>(colegio);
            newColegio.ModifiedBy = "Anonimous";

            _colegioService.Edit(p);

            var result = await _colegioService.Add(newColegio);

            await _colegioService.Save();

            return Ok(result);

        }

        // POST: api/Colegios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ColegioDTO>> PostColegio([FromBody] ColegioDTO colegio)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto No valido");
            }
            var p = _mapper.Map<Colegio>(colegio);

            p.ModifiedBy = "Anonimous";
            p.Deleted = true;
            await _colegioService.Add(p);
            Console.Write("\n");
            Console.Write("Colegio creado: ", p.Estado," - ", p.Nombre, " - " , p.Deleted);
            Console.Write("\n");
            await _colegioService.Save();
            return Ok(_mapper.Map<ColegioDTO>(colegio));
        }

        // DELETE: api/Colegios/1
        //TODO: estado = false al eliminar.
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _colegioService.FindById(id);

            if (data == null) {
                return NotFound();
            }
            data.Estado = false;
            data.Modified = DateTime.Now;
            data.Deleted = false;
            data.ModifiedBy = "Anonimous";
            _colegioService.Edit(data);
               Console.Write("\n");
            Console.Write("Colegio eliminado = estado: {0}", data.Estado," -nombre: {1}", data.Nombre, " -deleted: {2}" , data.Deleted);
            Console.Write("\n");
            await _colegioService.Save();

            return Ok();
        }


    }
}