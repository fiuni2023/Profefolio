using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using profefolio.Models.DTOs.Auth;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services;

public class PersonasService : IPersona
{
    
    private readonly UserManager<Persona> _userManager;
    private readonly IHttpContextAccessor _httpContext;
    

    public PersonasService(UserManager<Persona> userManager, IHttpContextAccessor httpContext)
    {
        _userManager = userManager;
        _httpContext = httpContext;
    }
    public  Task<Persona> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Persona> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Persona> GetAll(int page, int cantPorPag)
    {
        return _userManager.Users
            .Where(p => !p.Deleted)
            .Skip(page)
            .Take(cantPorPag*page);
    }

    public Persona Edit(Persona t)
    {
        throw new NotImplementedException();
    }

    public  Task<Persona> Add(Persona t)
    {
        
        throw new NotImplementedException();
    }

    public  Task Save()
    {
        throw new NotImplementedException();
    }

    public int Count()
    {
        return _userManager.Users
            .Count(p => !p.Deleted);
    }

    public bool Exist()
    {
        return Count() > 0;
    }

  
    public async Task<Persona> FindById(string id)
    {
        var query = await _userManager.Users
            .Where(p => !p.Deleted && p.Id.Equals(id))
            .FirstOrDefaultAsync();

        if (query == null)
        {
            throw new FileNotFoundException();
        }
        return query;
    }

    public async Task<Persona> CreateUser(Persona user, string password)
    {
        
        if (await ExistMail(user.Email))
        {
            throw new BadHttpRequestException("El email al cual quiere registrarse ya existe");
        }
        
        var query = await _userManager.CreateAsync(user, password);
        
        if (query.Succeeded)
        {
            return await _userManager.FindByEmailAsync(user.Email);
        }

        throw new BadHttpRequestException("Error en la consulta");
    }

    public async Task<Persona> EditProfile(string id, Persona persona)
    {
        var query = await _userManager.Users
            .Where(p => !p.Deleted && p.Id.Equals(id))
            .FirstOrDefaultAsync();

        if (null == query)
        {
            throw new FileNotFoundException();
        }

        if (!(id.Equals(persona.Id)))
        {
            throw new BadHttpRequestException("Error al actualizar");
        }

        await _userManager.UpdateAsync(persona);

        return persona;
    }

    public async Task<bool> DeleteUser(string id)
    {
        var query = await _userManager.FindByIdAsync(id);

        if (query.Deleted)
        {
            throw new FileNotFoundException();
        }

        query.Deleted = true;

        await _userManager.UpdateAsync(query);
        return true;
    }

    public async Task<bool> ChangePassword(string id, ModelPassword newPassoword)
    {
        var query = await _userManager.FindByIdAsync(id);

        if (query.Deleted)
        {
            throw new FileNotFoundException();
        }

        await _userManager.RemovePasswordAsync(query);
        await _userManager.AddPasswordAsync(query, newPassoword.NewPassword);

        return true;
    }

    public async Task<bool> ExistMail(string email)
    {
        var query = await _userManager.FindByEmailAsync(email);

        return null == query ? false : !query.Deleted;
    }


    public void Dispose()
    {
        _userManager.Dispose();
    }
}