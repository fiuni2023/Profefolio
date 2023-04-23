using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using profefolio.Helpers;
using profefolio.Models.DTOs.Auth;
using profefolio.Models.Entities;
using profefolio.Repository;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace profefolio.Services;

public class AuthService : IAuth
{
    private readonly UserManager<Persona> _userManager;

    private readonly IColegio _colegioService;

    private readonly IConfiguration _configuration;

    public AuthService(UserManager<Persona> userManager, IConfiguration configuration, IColegio colegioService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _colegioService = colegioService;
    }
    public void Dispose()
    {
        _userManager.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<AuthPersonaDTO> Login(Login login)
    {
        var user = await _userManager
            .FindByEmailAsync(login.Email);            
        
        if ((user == null || user.Deleted) || !await _userManager.CheckPasswordAsync(user, login.Password))
            throw new BadHttpRequestException("Credenciales no validas");
        var roles = await _userManager.GetRolesAsync(user);

        if (roles.Contains("Administrador de Colegio"))
        {
            var colegio = await _colegioService.FindByIdAdmin(user.Id);
            if (colegio == null)
            {
                throw new BadHttpRequestException("El administrador no fue asignado a un colegio todavia");
            }
        }

        if (roles.Contains("Alumno"))
        {
            throw new UnauthorizedAccessException();
        }
       
        var authClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        foreach (var role in roles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        var token = new TokenGenerator(_configuration);

        var tokenValues = token.GetToken(authClaims);

        return new AuthPersonaDTO()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(tokenValues),
            Expires = tokenValues.ValidTo,
            Roles = (List<string>)roles,
            Email = login.Email

        };
    }

    public Task Logout()
    {
        throw new NotImplementedException();
    }
}