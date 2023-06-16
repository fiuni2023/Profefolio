using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using profefolio.Helpers;
using profefolio.Repository;
using profefolio.Models.DTOs.AnotacionAlumno;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using profefolio.Models.Entities;

namespace profefolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnotacionAlumnoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAnotacionAlumno _anotAlumnoService;
        private static int CantPorPage => Constantes.CANT_ITEMS_POR_PAGE;

        public AnotacionAlumnoController(IMapper mapper, IAnotacionAlumno anotAlumnoService)
        {
            _mapper = mapper;
            _anotAlumnoService = anotAlumnoService;
        }

        /* [HttpPost("get/")]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<List<AnotacionAlumnoResultDTO>>> GetAll([FromBody] AnotacionAlumnoGetDTO dto)
        {
            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Name);

                var result = await _anotAlumnoService.GetAllByAlumnoIdAndClaseId(dto.AlumnoId, dto.ClaseId, userEmail);

                return Ok(_mapper.Map<List<AnotacionAlumnoResultDTO>>(result));
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"{e}");
                return Unauthorized();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Error durante la obtencion de los datos");

            }
        } */

        [HttpPost("getWithInfo/")]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<AnotacionesWithInfoAlumnoResultDTO>> GetAllWithInfo([FromBody] AnotacionAlumnoGetDTO dto)
        {
            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Name);

                var (nombre, apellido, clase, ciclo, materias, anotaciones) = await _anotAlumnoService.GetAllWithInfoByAlumnoIdAndClaseId(dto.AlumnoId, dto.ClaseId, userEmail);

                var result = new AnotacionesWithInfoAlumnoResultDTO()
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Clase = clase,
                    Ciclo = ciclo,
                    Materias = materias,
                    Anotaciones = _mapper.Map<List<AnotacionAlumnoResultDTO>>(anotaciones)
                };
                return Ok(result);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"\n\n\n\n{e}\n\n\n\n");
                return Unauthorized(e.Message);
            }
            catch (BadHttpRequestException e)
            {
                Console.WriteLine($"{e}");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Error durante la obtencion de los datos");

            }
        }


        [HttpPost]
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> Post([FromBody] AnotacionAlumnoCreateDTO dto)
        {
            try
            {
                // validar que el profesor sea profesor de la materia clase
                // valdiar que el alumno sea del colegio de la clase 
                var userEmail = User.FindFirstValue(ClaimTypes.Name);

                if (await _anotAlumnoService.ValidarDatos(dto.ClaseId, userEmail, dto.AlumnoId))
                {
                    var alumno = await _anotAlumnoService.GetAlumnoByClaseAndIdColegioAlumno(dto.ClaseId, dto.AlumnoId);
                    if(alumno == null){
                        return NotFound("Alumno no encontrado la Clase");
                    }
                    var model = _mapper.Map<AnotacionAlumno>(dto);
                    model.AlumnoId = alumno.Id;
                    model.Created = DateTime.Now;
                    model.CreatedBy = userEmail;
                    model.Deleted = false;

                    await _anotAlumnoService.Add(model);
                    await _anotAlumnoService.Save();
                    return Ok();
                }

                return BadRequest("Datos incorrectos");

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Error al tratar de guardar");
            }
        }
    }
}