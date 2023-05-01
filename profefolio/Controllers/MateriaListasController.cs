﻿using System.Collections.ObjectModel;
using System.Security.Claims;
using AutoMapper;
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

        [HttpGet("ConProfesores/{idClase}")]
        public async Task<ActionResult<List<ClaseMateriaResultDTO>>> GetMateriasConProfesores(int idClase){
            var adminEmail = User.FindFirstValue(ClaimTypes.Name);
            try
            {
                var materiaLista = _materiaListaService.FindByIdClase(idClase, adminEmail);
                return Ok(_mapper.Map<ClaseMateriaResultDTO>(materiaLista));
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Sucedio un error inesperado durante la busqueda.");
            }
            
        }
    }




}
