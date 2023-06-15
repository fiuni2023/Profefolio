using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Helpers;
using profefolio.Models.DTOs.Anotacion;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnotacionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAnotacion _AnotacionService;

        public AnotacionController(IMapper mapper, IAnotacion AnotacionService)
        {
            _mapper = mapper;
            _AnotacionService = AnotacionService;
        }

        [HttpGet]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<IEnumerable<AnotacionResultDTO>>> GetAll()
        {
            try
            {

                var result = await _AnotacionService.GetAll();
                return Ok(_mapper.Map<List<AnotacionResultDTO>>(result));

            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                return BadRequest("Error durante la busqueda");

            }
        }

        [HttpGet("{idMateriaLista:int}")]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<IEnumerable<AnotacionResultDTO>>> GetAll(int idMateriaLista)
        {
            try
            {
                var emailProfesor = User.FindFirstValue(ClaimTypes.Name);

                var result = await _AnotacionService.GetAll(idMateriaLista, emailProfesor);
                return Ok(_mapper.Map<List<AnotacionResultDTO>>(result));

            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                return BadRequest("Error durante la busqueda");

            }
        }

        [HttpPost]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<AnotacionResultDTO>> Post([FromBody] AnotacionCreateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Peticion invalido");
            }

            try
            {
                var Anotacion = _mapper.Map<Anotacion>(dto);

                //var userId = User.Identity.GetUserId();
                var name = User.FindFirstValue(ClaimTypes.Name);

                if (!dto.Titulo.Trim().Any() || !dto.Contenido.Trim().Any())
                {
                    return BadRequest("Verifique que se haya completado los campos");
                }

                var verif = await _AnotacionService.VerificacionPreguardado(dto.MateriaListaId, name, dto.Titulo);

                if (!verif)
                {
                    return BadRequest("El titulo es repetido");
                }

                Anotacion.CreatedBy = name;
                Anotacion.Created = DateTime.Now;
                Anotacion.Deleted = false;

                await _AnotacionService.Add(Anotacion);
                await _AnotacionService.Save();

                return Ok(_mapper.Map<AnotacionResultDTO>(Anotacion));
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Error durante el guardado.");
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var name = User.FindFirstValue(ClaimTypes.Name);

                if (!(await _AnotacionService.VerificarProfesor(id, name)))
                {
                    return NotFound();
                }

                var Anotacion = await _AnotacionService.FindById(id);
                if (Anotacion == null)
                {
                    return BadRequest("Anotacion no encontrado");
                }

                Anotacion.ModifiedBy = name;
                Anotacion.Modified = DateTime.Now;
                Anotacion.Deleted = true;

                _AnotacionService.Edit(Anotacion);
                await _AnotacionService.Save();

                return Ok();

            }
            catch (Exception e)
            {
                _AnotacionService.Dispose();
                return BadRequest("Error durante la eliminacion");
            }
        }
    }
}