using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using profefolio.Helpers;
using profefolio.Repository;
using profefolio.Models.DTOs.ContactoEmergencia;
using Microsoft.AspNetCore.Authorization;
using profefolio.Models.Entities;
using System.Security.Claims;

namespace profefolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactoEmergenciaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IContactoEmergencia _contEmergService;
        private readonly IPersona _personaService;
        private static int CantPorPage => Constantes.CANT_ITEMS_POR_PAGE;

        public ContactoEmergenciaController(IMapper mapper, IContactoEmergencia contEmergService, IPersona personaService)
        {
            _mapper = mapper;
            _contEmergService = contEmergService;
            _personaService = personaService;
        }

        [HttpPost]
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> Post([FromBody] ContactoEmergenciaCreateDTO dto)
        {
            try
            {
                var alumno = await _personaService.FindById(dto.AlumnoId);

                var roles = await _personaService.GetRolesPersona(alumno);
                //se verifica que el id recibido sea de un alumno
                if (!roles.Contains("Alumno"))
                {
                    return BadRequest("El usuario no es un alumno");
                }

                var model = _mapper.Map<ContactoEmergencia>(dto);

                var name = User.FindFirstValue(ClaimTypes.Name);
                model.CreatedBy = name;
                model.Created = DateTime.Now;
                model.Deleted = false;

                var result = await _contEmergService.Add(model);
                await _contEmergService.Save();

                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");

                return BadRequest("Error durante el guardado");
            }
        }

        [HttpGet("{idAlumno}")]
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> GetAll(string idAlumno)
        {
            try
            {
                var result = await _contEmergService.GetAllByAlumno(idAlumno);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Error durante la obtencion de los contactos de emergencia");
            }
        }
    }
}