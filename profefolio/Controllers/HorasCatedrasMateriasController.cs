using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using profefolio.Helpers;
using profefolio.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using profefolio.Models.Entities;
using profefolio.Models.DTOs.HorasCatedrasMaterias;

namespace profefolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorasCatedrasMateriasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHorasCatedrasMaterias _horaCatMatService;
        private static int CantPorPage => Constantes.CANT_ITEMS_POR_PAGE;
        public HorasCatedrasMateriasController(IMapper mapper, IHorasCatedrasMaterias horaCMService)
        {
            _mapper = mapper;
            _horaCatMatService = horaCMService;
        }


        [HttpGet]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<List<HorariosColegiosResultDTO>>> GetAllColegiosHorarios()
        {
            var profeEmail = User.FindFirstValue(ClaimTypes.Name);

            var colegiosProfe = await _horaCatMatService.GetAllHorariosOfColegiosByEmailProfesor(profeEmail);
            var listaResult = new List<HorariosColegiosResultDTO>();

            try
            {
                foreach (var colProf in colegiosProfe)
                {

                    var colegioProfeResult = _mapper.Map<HorariosColegiosResultDTO>(colProf);
                    var horarios = colProf.Persona.ListaMaterias
                                .Select(c => _mapper.Map<HorarioMateriaDTO>(c.Horarios)).ToList();
                    colegioProfeResult.HorariosMaterias = horarios;
                    listaResult.Add(colegioProfeResult);
                }
                return Ok(listaResult);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Error durante la obtencion del horario");
            }
        }
    }
}