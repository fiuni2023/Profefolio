using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Materia;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Master")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IMateria _materiaService;
        private readonly int _cantPorPag = 10;
        private readonly IMapper _mapper;
        public MateriaController(IMateria materiaService, IMapper mapper)
        {
            _materiaService = materiaService;
            _mapper = mapper;
        }

        
        [HttpGet]
        [Route("page/{page}")]
        public ActionResult<DataListDTO<MateriaResultDTO>> GetMaterias(int page)
        {
            var query = _materiaService.GetAll(page, _cantPorPag);
            int totalPage = (int)Math.Ceiling((double)_materiaService.Count() / _cantPorPag);

            var result = new DataListDTO<MateriaResultDTO>();

            var enumerable = query as Materia[] ?? query.ToArray();
            result.CantItems = enumerable.Length;
            result.CurrentPage = page > totalPage ? totalPage : page;
            result.Next = result.CurrentPage + 1 < totalPage;
            result.DataList = _mapper.Map<List<MateriaResultDTO>>(enumerable.ToList());
            result.TotalPage = totalPage;

            return Ok(result);
        }

        // GET: api/Materias/1
        //TODO: si data.delete = false no retornar.
        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaResultDTO>> GetMateria(int id)
        {
            var materia = await _materiaService.FindById(id);
            if (materia == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<MateriaResultDTO>(materia);
            return Ok(response);
        }

        // PUT: api/Materias/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // 
        // una solicitud PUT requiere que el cliente envíe toda la entidad actualizada, no solo los cambios.
        [HttpPut("{id}")]
        public async Task<ActionResult<MateriaResultDTO>> PutMateria(int id, MateriaDTO materia)
        {
            //verificar el modelo
            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto No valido");
            }
            //verificar que no sea nulo
            if (materia.Nombre_Materia == null)
            {
                return BadRequest("Nombre No valido");
            }
             //VERIFICAR REPETIDOS con nombre igual
            var verificarNombreMateria = await _materiaService.FindByNameMateria(materia.Nombre_Materia);
            if (verificarNombreMateria != null)
            {
                return BadRequest($"Ya existe una materia con el mismo nombre: ${materia.Nombre_Materia}.");
            }
            
            var p = await _materiaService.FindById(id);
            if (p == null)
            {
                return NotFound();
            }
            string userId = User.Identity.GetUserId();
            p.ModifiedBy = userId;
            p.Deleted = false;
            p.Modified = DateTime.Now;

            p.Nombre_Materia = materia.Nombre_Materia;
            p.Estado = materia.Estado;
            var query =  _materiaService.Edit(p);
            await _materiaService.Save();

            return Ok(_mapper.Map<MateriaResultDTO>(query));
        }

        // POST: api/Materias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MateriaResultDTO>> PostMateria([FromBody] MateriaDTO materia)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto No valido");
            }

              //VERIFICAR REPETIDOS con nombre igual
            var verificarNombreMateria = await _materiaService.FindByNameMateria(materia.Nombre_Materia);
            if (verificarNombreMateria != null)
            {
                return BadRequest($"Ya existe una materia con el mismo nombre: ${materia.Nombre_Materia}.");
            }
            try
            {
                var p = _mapper.Map<Materia>(materia);

                var userId = User.Identity.GetUserId();
                p.ModifiedBy = userId;
                p.Deleted = false;
                var saved = await _materiaService.Add(p);
                await _materiaService.Save();
                return Ok(_mapper.Map<MateriaResultDTO>(saved));
            }
            catch (BadHttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return BadRequest($"Error al crear la materia ${materia.Nombre_Materia}");
            }
        }

        // DELETE: api/Materias/1
        //TODO: estado = false al eliminar.
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _materiaService.FindById(id);

            if (data == null)
            {
                return NotFound();
            }
            
            data.Modified = DateTime.Now;
            data.Deleted = true;
            data.ModifiedBy = "Anonimous";
            _materiaService.Edit(data);
            await _materiaService.Save();

            return Ok();
        }


    }
}