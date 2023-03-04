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

    [Route("/login")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<AuthPersonaDTO>> Login([FromBody]Login dto)
    {
        return Ok( await _authService.Login(dto));
    }
}