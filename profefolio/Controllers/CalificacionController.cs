using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using profefolio.Models.DTOs.Calificacion;
using profefolio.Repository;

namespace profefolio.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "Profesor")]
public class CalificacionController : ControllerBase
{
    private ICalificacion _calificacionService;

    public CalificacionController(ICalificacion calificacionService)
    {
        _calificacionService = calificacionService;
    }

    [HttpGet]
    [Route("{idMateriaLista:int}")]
    public async Task<ActionResult<PlanillaDTO>> Get(int idMateriaLista)
    {
        try
        {
            var user = User.FindFirstValue(ClaimTypes.Name);

            var result = await _calificacionService.GetAll(idMateriaLista, user);
            return Ok(result);
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine(e);
            return Unauthorized();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Error al realizar la peticion");
        }
    }

    [HttpPut]
    [Route("{idMateriaLista}")]
    public async Task<ActionResult<PlanillaDTO>> Put(int idMateriaLista, [FromBody] CalificacionPutDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Objeto no valido");
        }

        switch (dto.Modo)
        {
            case "nn" :
                if (dto.NombreEvaluacion.IsNullOrEmpty())
                {
                    return BadRequest("Objeto no valido");
                }

                break;

        }
        try
        {
            var user = User.FindFirstValue(ClaimTypes.Name);
            var result = await _calificacionService.Put(idMateriaLista, dto, user);
            return Ok(result);
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine(e);
            return Unauthorized();
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
        catch (BadHttpRequestException e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Error en la peticion");
        }
    }
}
