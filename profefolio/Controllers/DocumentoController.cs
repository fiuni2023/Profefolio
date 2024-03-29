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
        
        public DocumentoController(IDocumento documentoService, IMapper mapper, IProfesor profesorService,
        IMateriaLista materiaListaService)
        {
            _documentoService = documentoService;
            _mapper = mapper;
            _profesorService = profesorService;
            _materiaListaService = materiaListaService;

        }
        /// <summary>
        /// Guarda un documento - post
        /// Solo un profesor puede realizar la peticion
        /// https://localhost:7063/api/Documento
        ///Body:
        ///     
        /// {
        ///     "nombre": "Documento prueba con otro prf",
        ///     "enlace": "https://docs.google.com/spreadsheets/d/1I1a4PQB-D4jdHaMS5GquJIqfezNeWXN0vTSxegyq_1A/edit?usp=sharing",
        ///     "MateriaListaId":3
        ///}
        /// </summary>
        /// <remarks>
        /// </remarks>
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
            if (materiaLista == null)
            {
                return BadRequest("Materia Lista no existe");
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
                var userEmail = User.FindFirstValue(ClaimTypes.Name);
                var profId = await _profesorService.GetProfesorIdByEmail(userEmail);

                var profesor = await _profesorService.GetProfesorByEmail(userEmail);
                //Si no es prf de la clase de MateriaLista no puede crear el doc
                var profesorClase = await _materiaListaService.GetProfesorOfMateria(documento.MateriaListaId, userEmail);
                if (profesorClase != profesor)
                {
                    return BadRequest("No puede matipular datos ajenos.");
                }

                var verificarNombreDocumento = await _documentoService.FindByNameDocumento(documento.Nombre, documento.MateriaListaId);
                if (verificarNombreDocumento != null)
                {
                    return BadRequest($"Ya existe un documento con ese nombre");
                }

                var p = _mapper.Map<Documento>(documento);
                p.CreatedBy = userEmail;
                p.Deleted = false;

                var saved = await _documentoService.Add(p);
                await _documentoService.Save();

                return Ok(_mapper.Map<DocumentoResultDTO>(saved));
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest("No es profesor de la materia.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el documento.");
            }
        }
        
        /// <summary>
        /// Retorna los documentos creados por el prf que hace la petición.
        /// get all documentos de una materia
        /// Solo un profesor puede realizar la peticion
        /// Se retornan todos los documentos pertenecientes a esa materia
        /// https://localhost:7063/api/Documento/all/{idMateriaLista}
        /// </summary>
        /// <remarks>
        /// </remarks>

        [HttpGet]
        [Route("all/{idMateriaLista}")]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<IEnumerable<DocumentoResultDTO>>> GetAll(int idMateriaLista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto no válido");
            }

            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Name);
                var profId = await _profesorService.GetProfesorIdByEmail(userEmail);
                var result = await _documentoService.GetAll(idMateriaLista, profId);

                return Ok(_mapper.Map<List<DocumentoResultDTO>>(result));

            }
            catch (Exception e)
            {
                return BadRequest("Error durante la busqueda");

            }
        }
        /// <summary>
        /// Retorna un documento creado por el prf que hace la petición.
        /// Solo un profesor puede realizar la peticion
        /// https://localhost:7063/api/Documento/{DocumentoId}
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpGet("{id}")]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<DocumentoResultDTO>> GetDocumento(int id)
        {
            var doc = await _documentoService.FindById(id);
            if (doc == null)
            {
                _log.Error("An error occurred in the Get method");
                return NotFound();
            }

            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            var profId = await _profesorService.GetProfesorIdByEmail(userEmail);

            var isProfesor = await _documentoService.FindProfesorIdByDocumento(doc.MateriaListaId, profId);

            if (isProfesor)
            {
                var response = _mapper.Map<DocumentoResultDTO>(doc);
                return Ok(response);
            }
            else
            {
                return BadRequest("No puede ver el documento de otro profesor.");
            }
        }

        /// <summary>
        /// Elimina un documento creado por el prf que hace la petición.
        /// Solo un profesor puede realizar la peticion
        /// https://localhost:7063/api/Documento/{DocumentoId}
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _documentoService.FindById(id);

            if (data == null)
            {
                return NotFound();
            }

            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            var profId = await _profesorService.GetProfesorIdByEmail(userEmail);

            var isProfesor = await _documentoService.FindProfesorIdByDocumento(data.MateriaListaId, profId);
            if (isProfesor)
            {
                data.Modified = DateTime.Now;
                data.Deleted = true;
                data.ModifiedBy = userEmail;

                _documentoService.Edit(data);
                await _documentoService.Save();

                return Ok();
            }
            else
            {
                return BadRequest("No puede eliminar el documento de otro profesor.");
            }
        }

        
        /// <summary>
        /// Edita un documento creado por el prf que hace la petición.
        /// Solo un profesor puede realizar la peticion
        /// https://localhost:7063/api/Documento/{DocumentoId}
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpPut("{id}")]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<DocumentoResultDTO>> PutDocumento(int id, DocumentoDTO documento)
        {
            try
            {
            // Verificar el modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objeto no válido");
                }

            // Buscar el documento por ID
            var p = await _documentoService.FindById(id);
            if (p == null)
            {
                return NotFound("No se encuentra el documento.");
            }

            // Verificar si MateriaListaId existe
            var materiaLista = await _materiaListaService.FindById(documento.MateriaListaId);
            if (materiaLista == null)
            {
                return BadRequest("Materia Lista no existe");
            }

            // Verificar el nombre del documento
            if (string.IsNullOrWhiteSpace(documento.Nombre))
            {
                return BadRequest("Nombre de documento no válido");
            }

            // Verificar el formato del enlace
            if (!Uri.IsWellFormedUriString(documento.Enlace, UriKind.Absolute))
            {
                return BadRequest("Formato de enlace no válido");
            }

            // Verificar documentos duplicados con el mismo nombre
            var verificarNombreDocumento = await _documentoService.FindByNameDocumento(documento.Nombre, documento.MateriaListaId);
            if (verificarNombreDocumento != null && verificarNombreDocumento.Id != id)
            {
                return BadRequest("Ya existe un documento con ese nombre");
            }

            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            var profId = await _profesorService.GetProfesorIdByEmail(userEmail);
            var isProfesor = await _documentoService.FindProfesorOfDocumento(id, userEmail);
            if (isProfesor == false)
            {
                return BadRequest("No puede editar el documento creado por otro profesor.");
            }

            var profesor = await _profesorService.GetProfesorByEmail(userEmail);
            //Si no es prf de la clase de MateriaLista no puede crear el doc
            var profesorClase = await _materiaListaService.GetProfesorOfMateria(documento.MateriaListaId, userEmail);
            if (profesorClase != profesor)
            {
                return BadRequest("No puede matipular datos ajenos.");
            }

            
            

        // Actualizar los datos del documento
        p.ModifiedBy = userEmail;
        p.Deleted = false;
        p.Modified = DateTime.Now;
        p.Nombre = documento.Nombre;
        p.Enlace = documento.Enlace;
        p.MateriaListaId = documento.MateriaListaId;

        // Guardar los cambios
        await _documentoService.Save();

        return Ok(_mapper.Map<DocumentoResultDTO>(p));
    }
    catch (Exception ex)
    {
        // Manejar cualquier error interno y devolver una respuesta apropiada
        return StatusCode(400, $"Ha ocurrido un error: {ex.Message}");
    }
}


    }
}