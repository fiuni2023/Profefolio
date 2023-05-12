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
        private IDashboardProfesor _dashBoardService;
        private IColegio _colegioService;
        private IMapper _mapper;

        private static int CantPorPage => Constantes.CANT_ITEMS_POR_PAGE;


        public DashboardProfesorController(IPersona personaService, IColegioProfesor colegioProfesorService, IDashboardProfesor dashboardProfesor, IColegio colegioService, IMapper mapper)
        {
            _personaService = personaService;
            _cProfService = colegioProfesorService;
            _colegioService = colegioService;
            _mapper = mapper;
            _dashBoardService = dashboardProfesor;
        }

        ///<summary>
        /// Retorna la lista de colegios donde el profesor este ensenhabdo en el anho actual y la lista de horarios con
        /// la clase que corresponde a cada horario.
        /// 
        ///</summary>
        ///<remarks>
        /// Ticket <a href="https://nande-y.atlassian.net/browse/PF-274">PF-274</a>
        ///
        /// Detalles de objeto retornado
        ///
        ///[
        ///
        ///  {
        ///
        ///    "id": 0, //id en la tabla ColegioProfesor
        ///
        ///    "nombre": "string", //nombre del colegio
        ///
        ///    "clases": [
        ///
        ///      "string" // nombres de clases
        ///
        ///    ],
        ///
        ///    "horarios": [
        ///
        ///      {
        ///
        ///        "id": 0, // id en la tabla de materia lista
        ///
        ///        "dia": "string", // dia de clases
        ///
        ///        "inicio": "string" // hora de inicio de clases
        ///
        ///      }
        ///
        ///    ]
        ///
        ///  }
        ///
        ///]
        ///
        ///</remarks>
        [HttpGet("colegios/")]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<List<ColegiosProfesorDbDTO>>> GetColegiosCard()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);

            try
            {
                var (colegiosProfesors, clases, horarios) = await _cProfService.FindAllClases(userEmail);

                var colegiosDTOs = new List<ColegiosProfesorDbDTO>();

                foreach (var cp in colegiosProfesors)
                {
                    var dtoCp = _mapper.Map<ColegiosProfesorDbDTO>(cp);

                    foreach (var clase in clases)
                    {
                        if (clase.ColegioId == cp.ColegioId)
                        {
                            dtoCp.Clases.Add(clase.Nombre);
                        }
                    }
                    foreach (var horario in horarios)
                    {
                        if (horario.MateriaLista.Clase.ColegioId == cp.ColegioId)
                        {
                            dtoCp.Horarios.Add(_mapper.Map<ClasesHorariosProfesorDbDTO>(horario));
                        }
                    }
                    colegiosDTOs.Add(dtoCp);
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



        [HttpGet]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult> Get([FromBody] GetDashboardOptionsDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Peticion invalida");
            }
            if (dto.Id <= 0)
            {
                return BadRequest("Identificador Invalido");
            }



            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Name);

                /*
                    "card-clases", "card-materias", "cards-materia", "horarios-clases", "eventos-clases", 
                    "eventos-materias", "lista-alumnos", "promedio-puntajes", "promedio-asistencias"
                */
                switch (dto.Opcion.ToLower())
                {
                    case "card-clases":
                        //id colegio, //anho
                        var clases = await _dashBoardService.GetClasesForCardClases(dto.Id, userEmail, dto.Anho);
                        
                        return Ok();
                    default:
                        return BadRequest("Opcion Invalida");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Error durante la obtencion de datos.");
            }
        }

    }
}