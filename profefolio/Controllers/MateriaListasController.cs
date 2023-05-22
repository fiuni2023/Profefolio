using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs.ClaseMateria;
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

        [HttpGet]
        [Route("{idClase:int}")]
        public async Task<ActionResult<ClaseDetallesDTO>> Get(int idClase)
        {
            try
            {
                //Obtenemos el username del usuario
                var user = User.Identity.Name;

                var result = await _materiaListaService.FindByIdClase(idClase, user);

                return Ok(result);


            }
            catch (BadHttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
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


        [HttpPut]
        public async Task<ActionResult> Put(MateriaListaPutDTO dto)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);

            try
            {
              var result = await _materiaListaService.Put(userEmail, dto);

              return result ? Ok() : BadRequest("Error al realizar la consulta");
            }

            catch(FileNotFoundException)
            {
                return NotFound();
            }

            catch(UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch(BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch(Exception)
            {
                return BadRequest("Error a realizar la peticion");
            }

        }
    }




}
