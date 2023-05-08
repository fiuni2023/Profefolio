using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs.Auth;
using profefolio.Repository;

namespace profefolio.Controllers;

[Authorize(Roles = "Master,Administrador de Colegio,Profesor")]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    private readonly IAuth _authService;
    public AuthController(IAuth authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Permite realizar el login de los distintos tipos de usuario tales como: Administrador Master, Administradores de Colegios y Profesores. Para que se puean loguear los Administradores de Colegios tienen que estar primeramente asignados a un Colegio.
    /// </summary>
    [Route("/login")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<AuthPersonaDTO>> Login([FromBody]Login dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            return Ok(await _authService.Login(dto));
        }
        catch (BadHttpRequestException e)
        {
            Console.WriteLine(e.Message);
            return BadRequest(e.Message);
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine(e.Message);
            return Unauthorized();
        }
        
    }
    
}