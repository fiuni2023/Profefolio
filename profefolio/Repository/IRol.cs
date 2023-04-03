using Microsoft.AspNetCore.Identity;
using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface IRol : IRepository<IdentityRole>
{
    Task<bool> AsignToUser(string rol, Persona p);
    Task<string> FindByRol(string rol);
    
}