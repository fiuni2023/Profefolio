using System.Collections.ObjectModel;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs.ClaseMateria;
using profefolio.Models.DTOs.Materia;
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
        private readonly IMapper _mapper;

        public MateriaListasController(IMateriaLista materiaListaService, IPersona profesorService, IMateria materiaService, IClase claseService, IMapper mapper)
        {
            _materiaListaService = materiaListaService;
            _profesorService = profesorService;
            _materiaService = materiaService;
            _claseService = claseService;
            _mapper = mapper;
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
            var user = User.Identity.Name;
            try
            {
                var query = await _materiaListaService.EditMateriaLista(dto, user);
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
                //Obtenemos el username del usuario
                var user = User.Identity.Name;

                var result = await _materiaListaService.FindByIdClase(idClase, user);


                //Mapeamos al tipo de objeto claseDetalle, tomando, el IdClase y la clase
                var response = _mapper.Map<ClaseDetallesDTO>(result[0]);


                //Obtenemos las materias
                var materias = result.ConvertAll(x => x.Materia);


                //Unificamos is es que hay materias repetidas
                var materiasUnified = materias.DistinctBy(x => x.Id);


                //Creamos el contenedor de materias con profesores
                var materiaProfesoresList = new List<MateriaProfesoresDTO>();

                foreach (var item in materiasUnified)
                {
                    var materiaProfesores = new MateriaProfesoresDTO();
                    materiaProfesores.IdMateria = item.Id;

                    if (item.Nombre_Materia == null)
                    {
                        return BadRequest("Nombre Materia == null");
                    }

                    materiaProfesores.Materia = item.Nombre_Materia;
                    var profesoresPerMateria = item.MateriaListas
                        .Select(x => x.Profesor).ToList();

                    materiaProfesores.Profesores = _mapper.Map<List<ProfesorSimpleDTO>>(profesoresPerMateria);

                    materiaProfesoresList.Add(materiaProfesores);
                }

                response.MateriaProfesores = materiaProfesoresList;

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

        /*
        * No tocar la ruta, la cambie porque el default era demasiado largo
        */
        [HttpGet("/api/lista/materias/ConProfesores/{idClase}")]
        public async Task<ActionResult<List<ClaseMateriaResultDTO>>> GetMateriasConProfesores(int idClase)
        {
            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Name);
                var userRole = User.FindFirstValue(ClaimTypes.Role);

                var materiaLista = await _materiaListaService.FindByIdClaseAndUser(idClase, userEmail, userRole);

                var dto = _mapper.Map<List<ClaseMateriaResultDTO>>(materiaLista);

                //se carga los profesores a cada materia de la lista
                materiaLista.ForEach(a =>
                {
                    var value = dto.FirstOrDefault(b => b.Id == a.Id);
                    if (value == null)
                    {
                        throw new FileNotFoundException("Error durante la obtencion de los Profesores de las Materias");
                    }
                    var prof = _mapper.Map<ClaseMateriaProfesorDTO>(a.Profesor);
                    value.Profesores.Add(prof);
                });
                return Ok(dto);
            }
            catch (FileNotFoundException e)
            {
                return NotFound(e);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Sucedio un error inesperado durante la busqueda.");
            }

        }
    }




}
