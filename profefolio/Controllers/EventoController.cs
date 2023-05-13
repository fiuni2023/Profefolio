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
        private readonly IEvento _EventoService;
        //private readonly IMateriaLista _materiaListaService;
        private static int _cantPorPag => Constantes.CANT_ITEMS_POR_PAGE;
        private readonly IMapper _mapper;
        //private readonly IClase _claseService;
        public MateriaController(IEvento eventoService, IMapper mapper)
        {
            _eventoService = eventoService;
            _mapper = mapper;
            
        }
    }

        // POST: api/Evento
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventoResultDTO>> PostEvento([FromBody] EventoDTO evento)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto No valido");
            }
            if (evento.MateriaId == null)
            {
                return BadRequest("Datos no validos");
            }
            
            //VERIFICAR REPETIDOS con nombre de colegio e id iguales
            
            //VERIFICAR ID materia
            /*var materia = await _colegioService.FindByPerson(colegio.PersonaId);
            if (persona == null)
            {
                return BadRequest($"No existe el administrador.");
            }*/
            try
            {
                var p = _mapper.Map<Evento>(evento);

                var userId = User.Identity.GetUserId();
                p.ModifiedBy = userId;
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