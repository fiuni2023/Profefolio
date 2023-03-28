using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services;

public class ColegiosService : IColegio
{
    private ApplicationDbContext _dbContext;

    private readonly UserManager<Persona> _userManager;

    private bool _disposed;

    public ColegiosService(ApplicationDbContext dbContext, UserManager<Persona> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }
    public async Task<Colegio> FindById(int id)
    {
        Console.Write("\nid a buscar: {0}", id);
        Console.Write("\n");
        return await _dbContext.Colegios
            .Where(p => !p.Deleted && p.Id == id)
            .FirstOrDefaultAsync();
    }

    /*
    * verifica si existe un colegio con el nombre y idpersona pasados como parametro.
    */
    public async Task<Colegio> FindByNamePerson(string name, string idPerson)
    {
        Console.Write("\nColegio a buscar: {0}", name);
        Console.Write("\n");
        return await _dbContext.Colegios
            .Where(p => !p.Deleted && p.Nombre == name && p.PersonaId == idPerson)
            .FirstOrDefaultAsync();
    }
    public async Task<Colegio> FindByNameColegio(string name)
    {
        Console.Write("\nColegio a buscar: {0}", name);
        Console.Write("\n");
        return await _dbContext.Colegios
            .Where(p => !p.Deleted && p.Nombre == name)
            .FirstOrDefaultAsync();
    }

    public async Task<Persona> FindByPerson(string id)
    {
        var query = await _userManager.Users
            .Where(p => !p.Deleted && p.Id.Equals(id))
            .FirstOrDefaultAsync();

        if (query == null)
        {
            return null;
        }
        return query;
    }

      public async Task<int> FindByPersonRol(string id)
    {
        var query = await _userManager.GetUsersInRoleAsync("Administrador de Colegio");

        return query
            .Count(p => !p.Deleted  && p.Id == id);
    }

    /*
    Recibe el id del supuesto administrador
    
    public async Task<Persona> FindByPersonRol(string id)
    {

        var query1 = await _dbContext.Roles
            .Where(p => p.Name == "Administrador de Colegio") 
            .FirstOrDefaultAsync();
        
        var query = await _dbContext.UserRoles
            .Where(p => p.RoleId == query1.Id   && p.UserId == id) //si se cumple es un administrador de colegio
            .FirstOrDefaultAsync();
        
        if (query == null)
        {
            return null;
        }
        return query;
    }*/

    public IEnumerable<Colegio> GetAll()
    {
        return _dbContext.Colegios.Where(p => !p.Deleted);
    }

    public Colegio Edit(Colegio t)
    {
        _dbContext.Entry(t).State = EntityState.Modified;
        return t;
    }

    public async Task<Colegio> Add(Colegio t)
    {
        var result = await _dbContext.Colegios.AddAsync(t);
        return result.Entity;
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public int Count()
    {
        return _dbContext.Colegios
            .Count(p => !p.Deleted);
    }

    public bool Exist()
    {
        return Count() > 0;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public IEnumerable<Colegio> GetAll(int page, int cantPorPag)
    {
        return _dbContext.Colegios
         .Where(p => !p.Deleted)
         .Skip(page * cantPorPag)
         .Take(cantPorPag);
    }

    public async Task<Colegio> FindByIdAdmin(string id)
    {
        return await _dbContext.Colegios.Include(p => p.personas).FirstOrDefaultAsync(
                    c => !c.Deleted 
                    && c.PersonaId != null 
                    && id.Equals(c.PersonaId)
                    && !c.personas.Deleted);
    }
}