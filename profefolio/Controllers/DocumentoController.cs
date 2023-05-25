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
        private static int _cantPorPag => Constantes.CANT_ITEMS_POR_PAGE;
        private readonly IMapper _mapper;
       // private readonly IClase _claseService;
        public DocumentoController(IDocumento documentoService, IMapper mapper,IProfesor profesorService)
        {
            _documentoService = documentoService;
            _mapper = mapper;
            _profesorService = profesorService;
           
        }

        [HttpPost]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<DocumentoResultDTO>> PostDocumento([FromBody] DocumentoDTO documento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto no válido");
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
                var verificarNombreDocumento = await _documentoService.FindByNameDocumento(documento.Nombre);
                if (verificarNombreDocumento != null)
                {
                    return BadRequest($"Ya existe un documento con ese nombre");
                }

                var userEmail = User.FindFirstValue(ClaimTypes.Name);
                var profId = await _profesorService.GetProfesorIdByEmail(userEmail);
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



    }
}