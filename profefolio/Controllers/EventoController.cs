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

        [HttpGet("{id}")]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<EventoResultFullDTO>> GetEvento(int id)
        {
            try
            {
                var evento = await _eventoService.FindById2(id);
                if (evento == null)
                {
                    return BadRequest("Evento inexistente");
                }

                var userEmail = User.FindFirstValue(ClaimTypes.Name);
                var profId = await _profesorService.GetProfesorIdByEmail(userEmail);

                if (profId != evento.ProfesorId)
                {
                    return BadRequest("No puede acceder al evento");
                }

                var response = _mapper.Map<EventoResultFullDTO>(evento);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _log.Error("An error occurred in the GetEvento method", ex);
                return BadRequest("Error inesperado durante la busqueda");
            }
        }

       
        [HttpGet]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<List<EventoResultFullDTO>>> GetAll()
        {
            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Name);
                var profId = await _profesorService.GetProfesorIdByEmail(userEmail);

                var eventos = await _eventoService.GetAll(profId);
                return Ok(_mapper.Map<List<EventoResultFullDTO>>(eventos));
            }
            catch (Exception e)
            {
                 _log.Error("An error occurred in the GetAll method", e);
                return BadRequest("Error inesperado durante la busqueda");
            }
        }

        /// <summary>
        /// Guarda un Evento. 
        /// Se recibe tipo evento, fecha, materia, clase, colegio
        /// https://localhost:7063/api/Evento
        /// </summary>
        /// <response code="200">Retorna un status 200 vacio</response>
        /// <remarks>
        /// </remarks>
        [HttpPost]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<EventoResultDTO>> PostEvento([FromBody] EventoDTO evento)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto No valido");
            }
            if (!(evento.Tipo.Equals("Examen") || evento.Tipo.Equals("Parcial")
            || evento.Tipo.Equals("Prueba sumatoria") || evento.Tipo.Equals("Evento")))
            {
                return BadRequest("Tipo de evento invalido");
            }

            if (evento.MateriaId == null || evento.Fecha == null || evento.ClaseId == null
            || evento.ColegioId == null || evento.Tipo == null)
            {
                return BadRequest("Datos no validos");
            }

            //VERIFICAR ID materia
            var materia = await _materiaService.FindById(evento.MateriaId);
            if (materia == null)
            {
                return BadRequest($"Datos invalidos.");
            }
            //VERIFICAR ID clase
            var clase = await _claseService.FindById(evento.ClaseId);
            if (clase == null)
            {
                return BadRequest($"Datos invalidos.");
            }
            //VERIFICAR ID colegio
            var colegio = await _colegioService.FindById(evento.ColegioId);
            if (colegio == null)
            {
                return BadRequest($"Datos invalidos.");
            }
            //verificar que no se agregue un evento a una clase que no sea del profesor logueado
            var userEmail = User.FindFirstValue(ClaimTypes.Name);

            var profId = await _profesorService.GetProfesorIdByEmail(userEmail);
            evento.ProfesorId = profId;

            if (clase.ColegioId != evento.ColegioId)
            {
                return BadRequest("No puede matipular datos ajenos.");
            }
            //verificar que el prf pertenezca al colegio al cual quiere agregar un evento
            Task<int> _exist = _colegioProfesorService.Count(evento.ColegioId, userEmail);
            var resultado = _exist.Result;
            if (resultado == 0)
            {
                return BadRequest("No puede agregar eventos en otros colegios.");
            }

            //verificar que no haya el mismo tipo de evento la misma fecha en la misma materia de una clase
            var verificarEvento = await _eventoService.FindByEventoRepetido(evento.Tipo, evento.Fecha,
            evento.ClaseId, evento.MateriaId, evento.ColegioId);
            if (verificarEvento != null)
            {
                return BadRequest($"Ya existe el evento.");
            }
            try
            {
                var p = _mapper.Map<Evento>(evento);
                p.CreatedBy = userEmail;
                p.Deleted = false;

                var saved = await _eventoService.Add(p);
                await _eventoService.Save();

                return Ok(_mapper.Map<EventoResultDTO>(saved));
            }
            catch (BadHttpRequestException e)
            {

                return BadRequest($"Error al crear el evento");
            }

        }
    }
}