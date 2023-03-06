using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using profefolio.Models;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.DTOs.Colegio;
using profefolio.Models.Entities;
using profefolio.Repository;

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


        [HttpGet]
        [Route("page/{page}")]
        public ActionResult<DataListDTO<PersonaDTO>> GetPersonas(int page)
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
        [HttpGet("{id}")]
        public async Task<ActionResult<ColegioDTO>> GetColegio(int id)
        {
            var colegio = await _colegioService.FindById(id);

            if (colegio == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<ColegioDTO>(colegio);

            return Ok(response);
        }

        // PUT: api/Personas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       /** [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, PersonaDTO persona)
        {
            if (id != persona.Id)
            {
                return BadRequest("Los ID no se actualizan");
            }

            var p = await _personaService.FindById(id);

            if (p == null) return NotFound();

            p.ModifiedBy = "Anonimous";
            p.Deleted = true;
            p.Modified = DateTime.Now;

            var newPersona = _mapper.Map<Persona>(persona);
            newPersona.ModifiedBy = "Anonimous";

            _personaService.Edit(p);

            var result = await _personaService.Add(newPersona);


            await _personaService.Save();

            return Ok(result);

        }
**/
        // POST: api/Colegios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ColegioDTO>> PostColegio([FromBody]ColegioDTO colegio)
        {

            if (!ModelState.IsValid) return BadRequest("Objeto No valido");

            var p = _mapper.Map<Colegio>(colegio);

            p.ModifiedBy = "Anonimous";

            await _colegioService.Add(p);

            await _colegioService.Save();
            return Ok(_mapper.Map<ColegioDTO>(colegio));
        }
/**
        // DELETE: api/Personas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {


            var data = await _personaService.FindById(id);

            if (data == null) return NotFound();

            data.Modified = DateTime.Now;
            data.Deleted = false;
            data.ModifiedBy = "Anonimous";
            _personaService.Edit(data);

            await _personaService.Save();

            return Ok();
        }

    **/
    }
}