using System;
using System.Collections.Generic;
using System.Linq;
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
    [Authorize(Roles = "Administrador de Colegio,Profesor")]
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
        public async Task<ActionResult<CicloResultDTO>> Get(int id)
        {
            if (id < 0)
            {
                return BadRequest("Id invalido");
            }

            try
            {
                var result = await _cicloService.FindById(id);

                return result != null
                        ? Ok(_mapper.Map<CicloResultDTO>(result))
                        : BadRequest("Id no encontrado");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Error durante la busqueda");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CicloResultDTO>> Post([FromBody] CicloDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto invalido");
            }

            try
            {

                if (await _cicloService.ExisitNombre(dto.Nombre))
                {
                    return BadRequest("Ya existe un ciclo con ese nombre");
                }

                var ciclo = _mapper.Map<Ciclo>(dto);

                var userId = User.Identity.GetUserId();
                ciclo.CreatedBy = userId;
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
        public async Task<ActionResult> Put(int id, [FromBody] CicloDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto invalido");
            }
            if (id < 0)
            {
                return BadRequest("Id invalido");
            }

            try
            {

                if (await _cicloService.ExisitOther(id, dto.Nombre))
                {
                    return BadRequest("Ya existe un ciclo con ese nombre");
                }
                var ciclo = await _cicloService.FindById(id);

                var userId = User.Identity.GetUserId();
                ciclo.ModifiedBy = userId;
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
                return BadRequest("Error durante la edicion");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest("Id invalido");
            }

            try
            {
                var ciclo = await _cicloService.FindById(id);
                if (ciclo == null)
                {
                    return BadRequest("Ciclo no encontrado");
                }

                string userId = User.Identity.GetUserId();
                ciclo.ModifiedBy = userId;
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