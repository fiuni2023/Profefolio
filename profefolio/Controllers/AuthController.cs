using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs.Auth;
using profefolio.Repository;

namespace profefolio.Controllers;

[Authorize(Roles = "Master,Administrador de Colegio,Profesor")]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAuth _authService;
    public AuthController(IMapper mapper, IAuth authService)
    {
        _mapper = mapper;
        _authService = authService;
    }

    [Route("[action]")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<AuthPersonaDTO>> Login(Login dto)
    {
        return await _authService.Login(dto);
    }
}