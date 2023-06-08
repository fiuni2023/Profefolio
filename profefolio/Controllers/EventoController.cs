using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Evento;
using profefolio.Models.Entities;
using profefolio.Repository;
using log4net;
using profefolio.Helpers;
using System.Security.Claims;

namespace profefolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(EventoController));
        private readonly IEvento _eventoService;
        private static int _cantPorPag => Constantes.CANT_ITEMS_POR_PAGE;
        private readonly IMapper _mapper;
        private readonly IMateria _materiaService;
        private readonly IClase _claseService;
        private readonly IColegio _colegioService;
        private readonly IColegioProfesor _colegioProfesorService;
        private readonly IProfesor _profesorService;

        public EventoController(IEvento eventoService, IMapper mapper, IMateria materiaService,
        IClase claseService, IColegio colegioService, IColegioProfesor colegioProfesorService,
        IProfesor profesorService)
        {
            _eventoService = eventoService;
            _mapper = mapper;
            _materiaService = materiaService;
            _claseService = claseService;
            _colegioService = colegioService;
            _colegioProfesorService = colegioProfesorService;
            _profesorService = profesorService;
        }
        
        [HttpPost]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<EventoResultDTO>> PostEvento([FromBody] EventoDTO evento)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto No valido");
            }

            var evalue = evento.Tipo != null && !(evento.Tipo.Equals("Examen")
                                                  || evento.Tipo.Equals("Parcial")
                                                  || evento.Tipo.Equals("Prueba sumatoria")
                                                  || evento.Tipo.Equals("Evento"));
            if (evalue)
            {
                return BadRequest("Tipo de evento invalido");
            }

            var user = User.FindFirstValue(ClaimTypes.Name);
            
            //verificar que el prf pertenezca al colegio al cual quiere agregar un evento
            var exist = _colegioProfesorService.Count(evento.ColegioId, user);
            var resultado = exist.Result;
            if (resultado == 0)
            {
                return BadRequest("No puede agregar eventos en otros colegios.");
            }

           
            try
            {
                var p = _mapper.Map<Evaluacion>(evento);
                p.CreatedBy = user;
                p.Deleted = false;
                var saved = await _eventoService.Add(p, user);
                await _eventoService.Save();

                return Ok("Guardado");
            }
            catch (BadHttpRequestException e)
            {

                return BadRequest($"Error al crear el evento");
            }

        }
    }
}