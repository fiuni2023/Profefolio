using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs.Ciclo;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CicloController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICiclo _cicloService;
        private const int CantPorPage = 20;

        public CicloController(IMapper mapper, ICiclo cicloService)
        {
            _mapper = mapper;
            _cicloService = cicloService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador de Colegio,Profesor")]
        public async Task<ActionResult<IEnumerable<CicloResultDTO>>> GetAll()
        {
            try
            {

                var result = await _cicloService.GetAll();
                return Ok(_mapper.Map<List<CicloResultDTO>>(result));

            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                return BadRequest("Error durante la busqueda");

            }
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Administrador de Colegio,Profesor")]
        public async Task<ActionResult<CicloResultDTO>> Get(int id)
        {
            try
            {
                var result = await _cicloService.FindById(id);

                return result != null
                        ? Ok(_mapper.Map<CicloResultDTO>(result))
                        : BadRequest("Ciclo no encontrado");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Error durante la busqueda");
            }
        }


        [HttpPost]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult<CicloResultDTO>> Post([FromBody] CicloDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Peticion invalido");
            }

            try
            {

                if (await _cicloService.ExisitNombre(dto.Nombre))
                {
                    return BadRequest("Ya existe un Ciclo con ese nombre");
                }

                var ciclo = _mapper.Map<Ciclo>(dto);

                //var userId = User.Identity.GetUserId();
                var name = User.FindFirstValue(ClaimTypes.Name);

                ciclo.CreatedBy = name;
                ciclo.Created = DateTime.Now;
                ciclo.Deleted = false;

                await _cicloService.Add(ciclo);
                await _cicloService.Save();

                return Ok(_mapper.Map<CicloResultDTO>(ciclo));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Error durante el guardado.");
            }
        }


        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult> Put(int id, [FromBody] CicloDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto invalido");
            }

            try
            {
                var exist = await _cicloService.ExisitOther(id, dto.Nombre);
                if (exist)
                {
                    return BadRequest("Ya existe un Ciclo con ese nombre");
                }
                var ciclo = await _cicloService.FindById(id);

                if (ciclo == null)
                {
                    return BadRequest("El Ciclo no encontrado");
                }

                var name = User.FindFirstValue(ClaimTypes.Name);
                ciclo.ModifiedBy = name;
                ciclo.Modified = DateTime.Now;
                ciclo.Deleted = false;
                ciclo.Nombre = dto.Nombre;


                _cicloService.Edit(ciclo);
                await _cicloService.Save();

                return NoContent();
            }
            catch (Exception e)
            {
                _cicloService.Dispose();
                Console.WriteLine(e);
                return BadRequest(e);
                //return BadRequest("Error durante la edicion");
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var ciclo = await _cicloService.FindById(id);
                if (ciclo == null)
                {
                    return BadRequest("Ciclo no encontrado");
                }

                var name = User.FindFirstValue(ClaimTypes.Name);
                ciclo.ModifiedBy = name;
                ciclo.Modified = DateTime.Now;
                ciclo.Deleted = true;

                _cicloService.Edit(ciclo);
                await _cicloService.Save();

                return Ok();

            }
            catch (Exception e)
            {
                _cicloService.Dispose();
                Console.WriteLine(e);
                return BadRequest("Error durante la eliminacion");
            }
        }
    }
}