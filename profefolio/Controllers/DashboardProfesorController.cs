using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Helpers;
using profefolio.Repository;
using profefolio.Models.DTOs.DashboardProfesor;

namespace profefolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardProfesorController : ControllerBase
    {
        private IPersona _personaService;
        private IColegioProfesor _cProfService;
        private IColegio _colegioService;
        private IMapper _mapper;

        private static int CantPorPage => Constantes.CANT_ITEMS_POR_PAGE;


        public DashboardProfesorController(IPersona personaService, IColegioProfesor colegioProfesorService, IColegio colegioService, IMapper mapper)
        {
            _personaService = personaService;
            _cProfService = colegioProfesorService;
            _colegioService = colegioService;
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<ColegiosProfesorDbDTO>> GetColegiosCard()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);

            try
            {
                var (colegios, horarios) = await _cProfService.FindAllClases(userEmail);

                var colegiosDTOs = new List<ColegiosProfesorDbDTO>();

                foreach (var colegio in colegios)
                {
                    var colegioDto = _mapper.Map<ColegiosProfesorDbDTO>(colegio);

                    foreach (var horario in horarios)
                    {
                        var horarioDto = _mapper.Map<ClasesHorariosProfesorDbDTO>(horario);
                        colegioDto.Clases.Add(horarioDto);
                    }
                    colegiosDTOs.Add(colegioDto);
                }
                
                return Ok(colegiosDTOs);

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"{e}");
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Error inesperado durante la busqueda");

            }


        }

    }
}