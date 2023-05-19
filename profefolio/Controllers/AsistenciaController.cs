using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
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

        [HttpGet]
        public async Task<ActionResult<AsistenciaResultDTO>> GetAll(int idMateriaLista)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            try
            {
                var alumnosColegios = await _asistenciaService.FindAll(idMateriaLista, userEmail);
                var result = _mapper.Map<AsistenciaResultDTO>(alumnosColegios);
                return Ok(result);
                
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