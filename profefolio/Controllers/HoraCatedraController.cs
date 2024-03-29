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
        /// 
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



        /// <summary>
        /// Guarda una hora catedra. Se recibe la hora de inicio y de fin, y tienen que estar en el formato de 24 horas de 00:00 hasta 23:59
        /// 
        /// </summary>
        /// <response code="200">Retorna un status 200 vacio</response>
        /// <remarks>
        /// Ticket <a href="https://nande-y.atlassian.net/browse/PF-276">PF-276</a>
        /// 
        /// Ejemplo de Body:
        ///
        ///     {
        ///        "inicio": "07:30",
        ///        "fin": "08:10"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [Authorize(Roles = "Administrador de Colegio,Profesor")]
        public async Task<ActionResult> Post([FromBody] HoraCatedraDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Peticion invaldia");
            }

            try
            {
                if (dto.Fin == null || dto.Inicio == null)
                {
                    return BadRequest("Ambas horas tienen que ser validas");
                }

                //se obtiene la diferencia entre la hora de fin e inicio (La operacion es: Fin - Inicio), la diferencia es entre los minutos
                var diferencia = DateTime.Parse(dto.Fin).Subtract(DateTime.Parse(dto.Inicio)).TotalMinutes;

                if (diferencia <= 0)
                {
                    return BadRequest("La hora de finalizacion tiene que ser mayor a la hora de inicio");
                }

                if ((await _horaCatedraService.Exist(dto.Inicio, dto.Fin)))
                {
                    return BadRequest("Ya existe la hora catedra con la mismo hora de inicio y fin");
                }

                var userEmail = User.FindFirstValue(ClaimTypes.Name);

                var horaCatedra = _mapper.Map<HoraCatedra>(dto);
                horaCatedra.Created = DateTime.Now;
                horaCatedra.CreatedBy = userEmail;
                horaCatedra.Deleted = false;

                var saved = await _horaCatedraService.Add(horaCatedra);
                await _horaCatedraService.Save();
                return Ok(_mapper.Map<HoraCatedraResultDTO>(saved));
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Erro durante el guardado");
            }
        }

    }
}