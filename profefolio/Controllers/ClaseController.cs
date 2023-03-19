using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs.Clase;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Controllers
{
    [ApiController]
    [Authorize(Roles = "Administrador de Colegio,Profesor")]
    [Route("api/[controller]")]
    public class ClaseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClase _claseService;
        private readonly ICiclo _cicloService;
        private readonly IColegio _colegioService;
        private const int CantPorPage = 20;

        public ClaseController(IMapper mapper, IClase claseService, ICiclo cicloService, IColegio colegioService)
        {
            _mapper = mapper;
            _claseService = claseService;
            _cicloService = cicloService;
            _colegioService = colegioService;
        }


        [HttpPost]
        public async Task<ActionResult<ClaseResultDTO>> Post([FromBody] ClaseDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto invalido");
            }

            try
            {
                var clase = _mapper.Map<Clase>(dto);
                
                var ciclo = await _cicloService.FindById(dto.CicloId);
                if(ciclo == null){
                    return BadRequest("El ciclo no existe");
                }

                var colegio = await _colegioService.FindById(dto.ColegioId);
                if(colegio == null){
                    return BadRequest("El colegio no existe");
                }

                if(dto.Anho <= 0){
                    return BadRequest("Anho invalido");
                }


                var userId = User.Identity.GetUserId();
                clase.CreatedBy = userId;
                clase.Created = DateTime.Now;
                clase.Deleted = false;

                var result = await _claseService.Add(clase);
                
                await _claseService.Save();

                return Ok(_mapper.Map<ClaseResultDTO>(result));
            }
            catch (Exception e)
            {
                _claseService.Dispose();
                Console.WriteLine(e);
                return BadRequest("Error durante el guardado.");
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
                var clase = await _claseService.FindById(id);
                if (clase == null)
                {
                    return NotFound();
                }

                string userId = User.Identity.GetUserId();
                clase.ModifiedBy = userId;
                clase.Modified = DateTime.Now;
                clase.Deleted = true;

                _claseService.Edit(clase);
                await _claseService.Save();

                return Ok();

            }
            catch (Exception e)
            {
                _claseService.Dispose();
                Console.WriteLine(e);
                return BadRequest("Error durante la eliminacion");
            }
        }
    
    }
}