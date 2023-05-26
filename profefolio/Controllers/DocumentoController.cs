using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Documento;
using profefolio.Models.Entities;
using profefolio.Repository;
using log4net;
using profefolio.Helpers;
using System.Security.Claims;

namespace profefolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(DocumentoController));
        private readonly IDocumento _documentoService;
        private readonly IProfesor _profesorService;
        private readonly IMateriaLista _materiaListaService;
        private static int _cantPorPag => Constantes.CANT_ITEMS_POR_PAGE;
        private readonly IMapper _mapper;
       // private readonly IClase _claseService;
        public DocumentoController(IDocumento documentoService, IMapper mapper,IProfesor profesorService,
        IMateriaLista materiaListaService)
        {
            _documentoService = documentoService;
            _mapper = mapper;
            _profesorService = profesorService;
            _materiaListaService = materiaListaService;
           
        }

        [HttpPost]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<DocumentoResultDTO>> PostDocumento([FromBody] DocumentoDTO documento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto no válido");
            }
            //verificar si MateriaListaId existe
            var materiaLista = await _materiaListaService.FindById(documento.MateriaListaId);
            if(materiaLista == null)
            {
                return BadRequest("Materia Lista no existe");
            }
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            var profId = await _profesorService.GetProfesorIdByEmail(userEmail);

            var profesor = await _profesorService.GetProfesorByEmail(userEmail);
            //Si no es prf de la clase de MateriaLista no puede crear el doc
            var profesorClase = await _materiaListaService.GetProfesorOfMateria(documento.MateriaListaId, userEmail);
            if (profesorClase != profesor)
            {
                return BadRequest("No puede matipular datos ajenos.");
            }
            if (string.IsNullOrWhiteSpace(documento.Nombre))
            {
                return BadRequest("Nombre de documento no válido");
            }

            if (!Uri.IsWellFormedUriString(documento.Enlace, UriKind.Absolute))
            {
                return BadRequest("Formato de enlace no válido");
            }

            try
            {
                var verificarNombreDocumento = await _documentoService.FindByNameDocumento(documento.Nombre, documento.MateriaListaId);
                if (verificarNombreDocumento != null)
                {
                    return BadRequest($"Ya existe un documento con ese nombre");
                }

                
                documento.ProfesorId = profId;

                var p = _mapper.Map<Documento>(documento);
                p.CreatedBy = userEmail;
                p.Deleted = false;

                var saved = await _documentoService.Add(p);
                await _documentoService.Save();

                return Ok(_mapper.Map<DocumentoResultDTO>(saved));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el documento: {ex.Message}");
            }
        }

           /// <summary>
        /// Retorna los documentos creados por el prf que hace la petición.
        /// Solo un profesor puede realizar la peticion
        /// Se retornan todos los documentos pertenecientes a esa materia
        /// https://localhost:7063/api/Evento
        ///Body del Get:
        ///     
        ///     {
        ///         "id": 0, //id materia
        ///     
        ///     }
        /// </summary>
        /// <remarks>
        /// </remarks>
        
        [HttpGet]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<IEnumerable<DocumentoResultDTO>>> GetAll( [FromBody] DocumentoOpcionesDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto no válido");
            }
            
            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Name);
                var profId = await _profesorService.GetProfesorIdByEmail(userEmail);
                var result = await _documentoService.GetAll(dto.Id, profId);

                return Ok(_mapper.Map<List<DocumentoResultDTO>>(result));

            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                return BadRequest("Error durante la busqueda");

            }
        }


    }
}