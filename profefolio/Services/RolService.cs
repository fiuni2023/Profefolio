using Microsoft.AspNetCore.Identity;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services;

public class RolService: IRol
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<Persona> _userManager;
    public RolService(RoleManager<IdentityRole> roleManager, UserManager<Persona> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }
    public void Dispose()
    {
      _roleManager.Dispose();
    }

    public Task<IdentityRole> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IdentityRole> GetAll()
    {
        throw new NotImplementedException();
    }

    public IdentityRole Edit(IdentityRole t)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityRole> Add(IdentityRole t)
    {
        throw new NotImplementedException();
    }

    public Task Save()
    {
        throw new NotImplementedException();
    }

    public int Count()
    {
        throw new NotImplementedException();
    }

    public bool Exist()
    {
        throw new NotImplementedException();
    }

   

    public async Task<bool> AsignToUser(string rol, Persona p)
    {
        
        await _userManager.AddToRoleAsync(p, rol);
        return true;
    }

    public async Task<string> FindByRol(string rol)
    {
        var role = await _roleManager.FindByNameAsync(rol);
        return role.Name;
    }
}