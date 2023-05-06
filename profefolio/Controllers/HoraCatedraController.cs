using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Helpers;
using profefolio.Models.DTOs.HoraCatedra;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HoraCatedraController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHoraCatedra _horaCatedraService;
        private static int CantPorPage => Constantes.CANT_ITEMS_POR_PAGE;

        public HoraCatedraController(IMapper mapper, IHoraCatedra horaCatedraService)
        {
            _mapper = mapper;
            _horaCatedraService = horaCatedraService;
        }

        /// <summary>
        /// Obtiene todos los registros de HoraCatedra.
        /// 
        /// </summary>
        /// <returns>Lista de registros</returns>
        /// <remarks>
        /// Ticket <a href="https://nande-y.atlassian.net/browse/PF-275">PF-275</a>
        /// </remarks>
        /// <remarks>
        /// Ejemplo de respuesta:
        ///
        ///     {
        ///        "id": 1,
        ///        "Inicio": "07:30",
        ///        "Fin": "08:10"
        ///     }
        ///
        /// </remarks>
        [HttpGet]
        [Authorize(Roles = "Administrador de Colegio,Profesor")]
        public async Task<ActionResult<List<HoraCatedraResultDTO>>> GetAll()
        {
            try
            {
                var horas = await _horaCatedraService.FindAll();
                return Ok(_mapper.Map<List<HoraCatedraResultDTO>>(horas));
            }
            catch (Exception e)
            {

                Console.WriteLine($"{e}");
                return BadRequest("Error inesperado durante la busqueda");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador de Colegio,Profesor")]
        public async Task<ActionResult> Post([FromBody]HoraCatedraDTO dto){
            if(!ModelState.IsValid){
                return BadRequest("Peticion invaldia");
            }

            try{
                var userEmail = User.FindFirstValue(ClaimTypes.Name);
                
                var horaCatedra = _mapper.Map<HoraCatedra>(dto);
                horaCatedra.Created = DateTime.Now;
                horaCatedra.CreatedBy = userEmail;
                horaCatedra.Deleted = false;
                
                await _horaCatedraService.Add(horaCatedra);
                await _horaCatedraService.Save();
                return Ok();
            }catch(Exception e){
                Console.WriteLine($"{e}");
                return BadRequest("Erro durante el guardado");
            }
        }

    }
}