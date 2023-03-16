using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Master")]
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
            Console.Write("Colegio: ", colegio);
            if (colegio == null)
            {
                Console.Write("Colegio == null");
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
            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto No valido");
            }
            if (colegio.PersonaId == null)
            {
                return BadRequest("Administrador No valido");
            }
            
            //VERIFICAR ID
            var persona = await _colegioService.FindByPerson(colegio.PersonaId);
             if (persona == null)
            {
                return BadRequest($"No existe el administrador con id ${colegio.PersonaId}.");
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

            //var newColegio = _mapper.Map<Colegio>(colegio);
            //newColegio.ModifiedBy = "Anonimous";
            p.Nombre = colegio.Nombre;
            p.PersonaId = colegio.PersonaId;
            _colegioService.Edit(p);

           // var result = await _colegioService.Add(newColegio);

            await _colegioService.Save();

            return Ok("Colegio: " + p.Nombre + ",PersonaId: " + p.PersonaId );

        }

        // POST: api/Colegios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ColegioResultDTO>> PostColegio([FromBody] ColegioDTO colegio)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto No valido");
            }
            if (colegio.PersonaId == null)
            {
                return BadRequest("Colegio No valido");
            }
            //VERIFICAR REPETIDOS
            var verificar = await _colegioService.FindByNamePerson(colegio.Nombre, colegio.PersonaId);
            if (verificar != null)
            {
                return BadRequest($"Ya existe el colegio ${colegio.Nombre}.");
            }
            //VERIFICAR ID
            var persona = await _colegioService.FindByPerson(colegio.PersonaId);
             if (persona == null)
            {
                return BadRequest($"No existe el administrador con id ${colegio.PersonaId}.");
            }
            try
            {
                var p = _mapper.Map<Colegio>(colegio);

                var userId = User.Identity.GetUserId();
                p.ModifiedBy = userId;
                p.Deleted = false;
                await _colegioService.Add(p);
                await _colegioService.Save();
                return Ok("Colegio: " + p.Nombre + ", Id: " + p.Id + ", PersonaId: " + p.PersonaId);
            }
            catch (BadHttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return BadRequest($"Error al crear el colegio ${colegio.Nombre}");
            }

            //return BadRequest($"Error al crear el colegio ${colegio.Id}");
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


    }
}