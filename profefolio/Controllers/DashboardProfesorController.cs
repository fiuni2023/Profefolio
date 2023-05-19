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
using profefolio.Models.DTOs.DashboardProfesor.GetWithOpcions;

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


        public DashboardProfesorController(IPersona personaService, IColegioProfesor colegioProfesorService,
        IDashboardProfesor dashboardProfesor, IColegio colegioService, IMapper mapper,
        IProfesor profesorService)
        {
            _personaService = personaService;
            _cProfService = colegioProfesorService;
            _colegioService = colegioService;
            _mapper = mapper;
            _dashBoardService = dashboardProfesor;
            _profesorService = profesorService;
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


        ///<summary>
        ///Dependiendo de las opciones mandadas se retornara los datos necesarios para cargar en los componentes del dashboard de Profesor 
        /// 
        /// 
        ///</summary>
        ///
        ///<remarks>
        ///Body del Get:
        ///     
        ///     {
        ///         opcion: "opcion",
        ///         id: 0,
        ///         anho: 2023
        ///     
        ///     }
        ///     
	    /// Descripcion:
        ///
		///     * opcion: "card-clases", "card-materias", "cards-materia", "horarios-clases", "eventos-clases", "eventos-materias", "lista-alumnos", "promedio-puntajes", "promedio-asistencias"
        ///
		///     * id: el id mandado dependera de la opcion:
        ///
		///		        - card-clases: pasar en el id del colegio.
        ///
		///		        - card-materias: pasar el id de la clase.
        ///
		///			    - cards-materia: pasar el id de la materia.
        ///
		///			    - horarios-clases: pasar id del colegio.
        ///
		///		    	- eventos-clases: pasar id del colegio.
        ///
		///			    - eventos-materias: pasar id de la clase.
        ///
		///			    - lista-alumnos: pasar id de la materia en MateriaLista.
        ///
		///	    		- promedio-puntajes: pasar id de la materia en MateriaLista.
        /// 
		/// 			- promedio-asistencias: pasar id de la materia en MateriaListas.
        ///
		///		* anho: año del cual se quiere obtener los datos, sera util en la pagina de clase donde se tiene que traer las clases del profesor dentro de un colegio en el año actual, en los demas casos no se tedra en cuenta. 
        ///
        ///
        /// ***************************************************************************************************************************
        /// Caso de Opcion "card-clases"
        ///
        ///
        /// Ticket <a href="https://nande-y.atlassian.net/browse/PF-292">PF-292</a>
        ///     
        /// Body:
        ///     
		///		{
		///			opcion: "card-clases",
		///			id: 0,                              // id colegio
		///			anho: 2023                          // necesario
		///		}
        ///     
        /// Respuesta:
        /// 
        ///     [
		///			{
		///				id: 1,                          // id de clase
		///				ciclo: "1er ciclo",             // nombre del ciclo
		///				nombre: "Primer Grado",         //nombre de clase
		///				anho: 2023,                     //año de la clase
		///				alumnos: 10,                    //cantidad de alumnos de la clase
		///				materias: [
		///					"Matematicas", "Ciencias", "Quimica", "Historia"
		///				],
		///				horario: {                      //horario mas proximo al dia actual
		///					dia: "Jueves",
		///					inicio: "08:00",
		///					horas: "3hs"                // horas de clase en la clase ese dia 
		///				} 			
		///			}
		///		]
        ///     
        ///     
        ///     
        /// ***************************************************************************************************************************
        /// Caso de Opcion "horarios-clases"
        ///        
        ///
        /// Ticket <a href="https://nande-y.atlassian.net/browse/PF-294">PF-294</a>
        ///     
        /// Body:
        ///     
		///		{
		///			opcion: "horarios-clases",
		///			id: 1,                              // id colegio
		///			anho: 2023                          // necesario
		///		}      
        ///     
        ///     
        /// Respuesta:
        ///     
        ///     [
        ///         {
        ///             "nombre": "Septimo Grado",
        ///             "dia": "Lunes",
        ///             "inicio": "13:40",
        ///             "fin": "14:20",
        ///             "id": 2                         // id de la clase
        ///         }
        ///     ]     
        ///     
        ///     
        ///     
        /// ***************************************************************************************************************************
        ///
        /// Caso de Opcion "cards-materia"
        ///        
        ///
        /// Ticket <a href="#">Sin ticket hasta el momento</a>
        ///     
        /// Body:
        ///     
		///		{
		///			opcion: "cards-materia",
		///			id: 1,                              // id materiaLista
		///			anho: 2023                          
		///		}      
        ///     
        ///     
        /// Respuesta:
        ///     
        ///     {
		///			anotaciones: 25,
		///			calificaciones: { 
		///				calificaciones:	4,
		///				sinCalificaciones: 3
		///			},
		///			asistencias: 8,
		///			documentos: 4
		///		}    
        ///     
        ///     
        ///     
        /// ***************************************************************************************************************************
        ///
        ///
        /// Caso de Opcion "promedio-puntajes"
        ///        
        ///
        /// Ticket <a href="#">Sin ticket hasta el momento</a>
        ///     
        /// Body:
        ///     
		///		{
		///			opcion: "promedio-puntajes",
		///			id: 1,                              // id materiaLista
		///			anho: 2023                          
		///		}      
        ///     
        ///     
        /// Respuesta:
        ///     
        ///     [
		///			{
		///				evaluacion: "Pueba 1",
		///				puntaje: 30.0
		///			},
		///		]   
        ///     
        ///     
        ///     
        /// ***************************************************************************************************************************
        ///
        /// Caso de Opcion "promedio-asistencias"
        ///        
        ///
        /// Ticket <a href="#">Sin ticket hasta el momento</a>
        ///     
        /// Body:
        ///     
		///		{
		///			opcion: "promedio-asistencias",
		///			id: 1,                              // id materiaLista
		///			anho: 2023                          
		///		}      
        ///     
        ///     
        /// Respuesta:
        ///     
        ///     [
		///			{
		///				mes: "enero",
		///				presentes: 30.0,
		///				ausentes: 20.0,
		///				justificados: 10.0
		///			}
		///		]   
        ///     
        ///     
        ///     
        /// ***************************************************************************************************************************
        ///
        ///</remarks>
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

            //DateTime.Now.

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
                        var (profesor, clases) = await _dashBoardService.GetClasesForCardClases(dto.Id, userEmail, dto.Anho);
                        var results = _mapper.Map<List<DBCardClasesDTO>>(clases);

                        foreach (var result in results)
                        {
                            result.Materias = await _dashBoardService.FindMateriasOfClase(profesor, result.Id);
                            var horarioMasCercano = await _dashBoardService.FindHorarioMasCercano(profesor, result.Id);
                            if (horarioMasCercano != null)
                            {
                                result.Horario = _mapper.Map<DBCardClasesHorariosDTO>(horarioMasCercano);
                                result.Horario.Horas = await _dashBoardService.GetHorasOfClaseInDay(profesor, result.Id, horarioMasCercano.Dia);
                            }
                        }

                        return Ok(results);

                    case "horarios-clases":
                        var horarios = await _dashBoardService.FindAllHorariosClasesByEmailProfesorAndIdColegio(dto.Id, userEmail, dto.Anho);
                        return Ok(_mapper.Map<List<DBHorariosClasesCalendarDTO>>(horarios));

                    case "card-materias":
                        //id clase, //anho

                        var profId = await _profesorService.GetProfesorIdByEmail(userEmail);
                        var profesor2 = await _personaService.FindById(profId);
                        var materiasClase = await _dashBoardService._FindMateriasOfClase(profesor2, dto.Id);

                        var resultsMaterias = _mapper.Map<List<DBCardMateriasDTO>>(materiasClase);

                        foreach (var result in resultsMaterias)
                        {
                            result.Eventos = await _dashBoardService.GetEventosOfMateria(profId,
                            result.MateriaId, dto.Id);

                            var horarioMasCercano = await _dashBoardService.FindHorarioMasCercanoMateria(profesor2,
                            result.Id);

                            if (horarioMasCercano != null)
                            {
                                result.Horario = _mapper.Map<DBCardClasesHorariosDTO>(horarioMasCercano);
                                result.Horario.Horas = await _dashBoardService.GetHorasOfMateriaInDay(profesor2,
                                result.Id, horarioMasCercano.Dia);
                            }
                        }

                        return Ok(resultsMaterias);


                    case "cards-materia":
                        var materia = await _dashBoardService.FindDataForCardOfInfoMateria(dto.Id, userEmail);
                        return Ok(_mapper.Map<DBCardsMateriaInfo>(materia));


                    case "eventos-clases":
                        return BadRequest("Opcion en implementacion");
                    case "eventos-materias":
                        return BadRequest("Opcion en implementacion");




                    case "lista-alumnos":
                        //id clase
                        //id prf

                        var _profId = await _profesorService.GetProfesorIdByEmail(userEmail);
                        var clasesA = await _dashBoardService.GetColegioAlumnoId(dto.Id, _profId);
                        var resultsA = _mapper.Map<List<DBClaseAlumnoColegioDTO>>(clasesA);

                        foreach (var result in resultsA)
                        {
                            //a partir del colegioAlumnoId obtener el idAlumno
                            var idAlumno = await _dashBoardService.FindAlumnoIdByColegioAlumnoId(result.Id);
                            var alumno = await _personaService.FindById(idAlumno);
                            result.Nombres = alumno.Nombre;
                            result.Apellidos = alumno.Apellido;

                        }
                        resultsA = resultsA.OrderBy(r => r.Apellidos).ToList();
                        return Ok(resultsA);
                    
                    
                    
                    case "promedio-puntajes":
                        /*
                            TODO

                            HACER QUE SE OBTENGAN LOS VALORES DE LA BASE DE DATOS DESPUES DE QUE SE CREEN LAS 
                            TABLAS RELACIONADAS A CALIFICACIONES
                        */
                        var promedios = await _dashBoardService.GetPromediosPuntajesByIdMateriaLista(dto.Id, userEmail);
                        
                        var promedio1 = new DBPromedioPuntajesDTO(){
                            Evaluacion = "Prueba 1",
                            Puntaje = 45.5
                        };
                        var promedio2 = new DBPromedioPuntajesDTO(){
                            Evaluacion = "Prueba 1",
                            Puntaje = 85.2
                        };
                        var promedio3 = new DBPromedioPuntajesDTO(){
                            Evaluacion = "Prueba 1",
                            Puntaje = 68.0
                        };
                        var promedio4 = new DBPromedioPuntajesDTO(){
                            Evaluacion = "Prueba 1",
                            Puntaje = 90.0
                        };
                        var listPromedios = new List<DBPromedioPuntajesDTO>(){promedio1, promedio2, promedio3, promedio4};
                        
                        return Ok(listPromedios);



                    case "promedio-asistencias":
                        /*
                            TODO

                            HACER QUE SE OBTENGAN LOS VALORES DE LA BASE DE DATOS DESPUES DE QUE SE CREEN LAS 
                            TABLAS RELACIONADAS A ASISTENCIAS
                        */
                        var promediosAsistencias = await _dashBoardService.GetPromediosAsistenciasByIdMateriaAndProfesorEmail(dto.Id, userEmail);

                        var promedioAsistencia1 = new DBPromedioAsistenciasDTO(){
                            Mes = "Enero",
                            Presentes = 50,
                            Ausentes = 10,
                            Justificados = 40
                        };
                        var promedioAsistencia2 = new DBPromedioAsistenciasDTO(){
                            Mes = "Febrero",
                            Presentes = 80,
                            Ausentes = 10,
                            Justificados = 210
                        };
                        var promedioAsistencia3 = new DBPromedioAsistenciasDTO(){
                            Mes = "Marzo",
                            Presentes = 90,
                            Ausentes = 5,
                            Justificados = 5
                        };
                        var promedioAsistencia4 = new DBPromedioAsistenciasDTO(){
                            Mes = "Abril",
                            Presentes = 70,
                            Ausentes = 10,
                            Justificados = 20
                        };

                        return Ok(new List<DBPromedioAsistenciasDTO>(){
                            promedioAsistencia1, promedioAsistencia2, promedioAsistencia3, promedioAsistencia4
                        });

                    default:
                        return BadRequest("Opcion Invalida");
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"{e}");
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Error durante la obtencion de datos.");
            }
        }

    }
}