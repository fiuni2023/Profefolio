using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Helpers;
using profefolio.Models.DTOs.Asistencia;
using profefolio.Repository;

namespace profefolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsistenciaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAsistencia _asistenciaService;
        private static int CantPorPage => Constantes.CANT_ITEMS_POR_PAGE;

        public AsistenciaController(IMapper mapper, IAsistencia asistencia)
        {
            _mapper = mapper;
            _asistenciaService = asistencia;
        }

        [HttpGet("{idMateriaLista:int}")]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<List<AsistenciaResultDTO>>> GetAll(int idMateriaLista)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            try
            {
                var alumnosColegios = await _asistenciaService.FindAll(idMateriaLista, userEmail);

                var results = new List<AsistenciaResultDTO>();

                foreach (var alumnoColegio in alumnosColegios.GroupBy(a => a.ColegiosAlumnosId).ToList())
                {


                    foreach (var item in alumnoColegio)
                    {
                        var resultDto = _mapper.Map<AsistenciaResultDTO>(item);
                        resultDto.Asistencias = item.Asistencias.OrderBy(a => a.Fecha)
                                .TakeWhile(a => a.Fecha > a.Fecha.AddDays(-5))
                                .Select(b => new AssitenciasFechaResult()
                                {
                                    Fecha = b.Fecha,
                                    Id = b.Id,
                                    Estado = b.Estado,
                                    Observacion = b.Observacion
                                })
                                .ToList();
                        results.Add(resultDto);
                    }

                }

                return Ok(results);

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"{e}");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Error durante la obtencion de asistencias");
            }
        }
    }
}