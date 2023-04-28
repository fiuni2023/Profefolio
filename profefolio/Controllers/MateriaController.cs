using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Materia;
using profefolio.Models.Entities;
using profefolio.Repository;
using log4net;
using profefolio.Helpers;
using System.Security.Claims;

namespace profefolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(MateriaController));
        private readonly IMateria _materiaService;
        private readonly IMateriaLista _materiaListaService;
        private static int _cantPorPag => Constantes.CANT_ITEMS_POR_PAGE;
        private readonly IMapper _mapper;
        public MateriaController(IMateria materiaService, IMapper mapper, IMateriaLista materiaLista)
        {
            _materiaService = materiaService;
            _mapper = mapper;
            _materiaListaService = materiaLista;
        }


        [HttpGet]
        [Authorize(Roles = "Administrador de Colegio,Profesor")]
        public async Task<ActionResult<List<MateriaResultDTO>>> GetAll(){
            try{
                var materias = await _materiaService.GetAll();
                if(materias == null){
                _log.Error($"Error durante la obtencion de las materias, la lista es nula.");
                    return BadRequest("Erro en la obtencion de materias.");
                }
                return Ok(_mapper.Map<List<MateriaResultDTO>>(materias));
            }catch(Exception e){
                _log.Error($"Error durante la obtencion de las materias: \n{e}");
                return BadRequest("Erro durante la obtencion de las materias");
            }
        }

        [HttpGet]
        [Route("page/{page}")]
        [Authorize(Roles = "Administrador de Colegio,Profesor")]
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
        [Authorize(Roles = "Administrador de Colegio,Profesor")]
        public async Task<ActionResult<MateriaResultDTO>> GetMateria(int id)
        {
            var materia = await _materiaService.FindById(id);
            if (materia == null)
            {
                _log.Error("An error occurred in the Get method");
                return NotFound();
            }

            var response = _mapper.Map<MateriaResultDTO>(materia);
            return Ok(response);
        }

        // get para obetner una lista de materias que no fueron asignadas a una clase
        [HttpGet("NoAsignadas/{idClase:int}")]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult<List<MateriaResultDTO>>> GetMateriasNoAsignadas(int idClase)
        {
            // obtener la lista de relaciones de la clase en MateriaLista
            var result = await _materiaService.FindAllUnsignedMaterias(idClase);

            return Ok(_mapper.Map<List<MateriaResultDTO>>(result));
        }

        // PUT: api/Materias/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // 
        // una solicitud PUT requiere que el cliente envíe toda la entidad actualizada, no solo los cambios.
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult<MateriaResultDTO>> PutMateria(int id, MateriaDTO materia)
        {
            //verificar el modelo
            if (!ModelState.IsValid)
            {
                _log.Error("An error occurred in the put method");
                return BadRequest("Objeto No valido");
            }
            //verificar que no sea nulo
            if (materia.Nombre_Materia == null || materia.Nombre_Materia == " " || materia.Nombre_Materia == "")
            {
                _log.Error("An error occurred in the put method");
                return BadRequest("Nombre de materia No valido");
            }

            var p = await _materiaService.FindById(id);
            if (p == null)
            {
                _log.Error("An error occurred in the put method");
                return NotFound();
            }
            //VERIFICAR REPETIDOS con nombre igual
            var verificarNombreMateria = await _materiaService.FindByNameMateriaId(materia.Nombre_Materia,id);
            if (verificarNombreMateria != null)
            {
                return BadRequest($"Ya existe una materia con el mismo nombre.");
            }
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            p.ModifiedBy = userEmail;
            p.Deleted = false;
            p.Modified = DateTime.Now;

            p.Nombre_Materia = materia.Nombre_Materia;
            var query = _materiaService.Edit(p);
            await _materiaService.Save();

            return Ok(_mapper.Map<MateriaResultDTO>(query));
        }

        // POST: api/Materias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult<MateriaResultDTO>> PostMateria([FromBody] MateriaDTO materia)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto No valido");
            }
            if (materia.Nombre_Materia == null || materia.Nombre_Materia == " " || materia.Nombre_Materia == "")
            {
                return BadRequest("Nombre de materia No valido");
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

                var userEmail = User.FindFirstValue(ClaimTypes.Name);
                p.ModifiedBy = userEmail;
                p.Deleted = false;
                var saved = await _materiaService.Add(p);
                await _materiaService.Save();
                return Ok(_mapper.Map<MateriaResultDTO>(saved));
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest($"Error al crear la materia.");
            }
        }

        // DELETE: api/Materias/1
        //TODO: estado = false al eliminar.
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<IActionResult> Delete(int id)
        {
            
            var data = await _materiaService.FindById(id);
            
            if (data == null)
            {
                return NotFound();
            }

            // se verifica que la materia no se este usando en la relacion de materia-clase
            var isUsed = await _materiaListaService.IsUsedMateria(id);
            if(isUsed){
                return BadRequest("La materia no se puede eliminar porque que ya se esta usando.");
            }
            
            var userEmail = User.FindFirstValue(ClaimTypes.Name);

            data.Modified = DateTime.Now;
            data.Deleted = true;
            data.ModifiedBy = userEmail;
            _materiaService.Edit(data);
            await _materiaService.Save();

            return Ok();
        }


    }
}