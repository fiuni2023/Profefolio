using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs.ClaseMateria;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Controllers
{
    [Route("api/administrador/materia/profesores")]
    [ApiController]
    [Authorize(Roles = "Administrador de Colegio")]
    public class MateriaListasController : ControllerBase
    {
        private readonly IMateriaLista _materiaListaService;
        private readonly IPersona _profesorService;
        private readonly IMateria _materiaService;
        private readonly IClase _claseService;

        public MateriaListasController(IMateriaLista materiaListaService, IPersona profesorService, IMateria materiaService, IClase claseService)
        {
            _materiaListaService = materiaListaService;
            _profesorService = profesorService;
            _materiaService = materiaService;
            _claseService = claseService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClaseMateriaCreateDTO dto)
        {
            var user = User.Identity.Name;
            try
            {
                var query = await _materiaListaService.SaveMateriaLista(dto, user);
                if (query) return Ok();
            }
            catch (FileNotFoundException e)
            {
                return NotFound();
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized();
            }
            return BadRequest("Error al ejecutar la secuencia de intrucciones");
        }


        //Este metodo GET NO SE DEBE IMPLEMENTAR EN EL FRONT-END, es con fines de Testing
        [HttpGet]
        public ActionResult GetAllTemp()
        {

            var query = _materiaListaService
                .GetAll(0, 0)
                .ToList()
                .ConvertAll(p => new
                {
                    Id = p.Id,
                    Profes = new
                    {
                        IdProfesor = p.ProfesorId,
                        ProfesorMail = p.Profesor.Email,
                    },
                    Clase = new
                    {
                        ClaseName = p.Clase.Nombre,
                        Id = p.ClaseId
                    },
                    Materia = new
                    {
                        Id = p.MateriaId,
                        Materia = p.Materia.Nombre_Materia
                    }

                });


            return Ok(query);

        }


        [HttpPut]
        [Route("{idClase:int}")]
        public async Task<ActionResult> Put(int idClase, [FromBody] ClaseMateriaEditDTO dto)
        {

            if (idClase != dto.IdClase)
            {
                return BadRequest();
            }

            if (dto.IdProfesores.Count < 1) return BadRequest();

            var user = User.Identity.Name;
            try
            {
                var clase = await _claseService.FindById(idClase);

                var detalles = new Collection<MateriaLista>();

                foreach (var item in dto.IdProfesores.Distinct())
                {
                    var p = await _profesorService.FindById(item);

                    if (p == null) { continue; }

                    var detalle = await _materiaListaService.Find(dto.IdClase, item, dto.IdMateria, user);

                    if (detalle == null)
                    {
                        detalle = new MateriaLista
                        {
                            ClaseId = dto.IdClase,
                            MateriaId = dto.IdMateria,
                            ProfesorId = item
                        };
                    }
                    else
                    {
                        detalle.ClaseId = dto.IdClase;
                        detalle.MateriaId = dto.IdMateria;
                        detalle.ProfesorId = item;
                    }

                    detalles.Add(detalle);

                }


                clase.MateriaListas = detalles;



                _claseService.Edit(clase);
                await _claseService.Save();
                return Ok();
            }
            catch (FileNotFoundException e)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{idClase:int}")]
        public async Task<ActionResult> DeleteByIdClase(int idClase)
        {
            try
            {
                var user = User.Identity.Name;

                await _materiaListaService.DeleteByIdClase(idClase, user);
            }
            catch (FileNotFoundException e)
            {
                return NotFound();
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route("{idClase:int}")]
        public async Task<ActionResult<ClaseDetallesDTO>> Get(int idClase)
        {
            try
            {
                var user = User.Identity.Name;
                var result = await _materiaListaService.FindByIdClase(idClase, user);

                var response = new ClaseDetallesDTO();

                response.IdProfesores = result.ConvertAll(c => c.ProfesorId);
                response.ClaseId = idClase;
                response.MateriaId = result[0].ClaseId;
                return Ok(response);

            }
            catch (BadHttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return NotFound();
            }
        }

    }




}
