using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using profefolio.Helpers;
using profefolio.Repository;
using profefolio.Models.DTOs.AnotacionAlumno;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace profefolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnotacionAlumnoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAnotacionAlumno _anotAlumnoService;
        private static int CantPorPage => Constantes.CANT_ITEMS_POR_PAGE;

        public AnotacionAlumnoController(IMapper mapper, IAnotacionAlumno anotAlumnoService)
        {
            _mapper = mapper;
            _anotAlumnoService = anotAlumnoService;
        }

        [HttpPost("/get")]
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> GetAll([FromBody] AnotacionAlumnoGetDTO dto)
        {
            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Name);

                var result = await _anotAlumnoService.GetAllByAlumnoIdAndMateriaListaId(dto.AlumnoId, dto.MateriaListaId, userEmail);

                return Ok(_mapper.Map<AnotacionAlumnoResultDTO>(result));
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"{e}");
                return Unauthorized();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Error durante la obtencion de los datos");

            }
        }

    }
}