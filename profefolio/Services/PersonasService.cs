using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services;

public class PersonasService : IPersona
{

    private readonly UserManager<Persona> _userManager;

    public PersonasService(UserManager<Persona> userManager)
    {
        _userManager = userManager;
    }
    public Task<Persona> FindById(int id)
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
            .Skip(page * cantPorPag)
            .Take(cantPorPag);
    }

    public Persona Edit(Persona t)
    {
        throw new NotImplementedException();
    }

    public Task<Persona> Add(Persona t)
    {

        throw new NotImplementedException();
    }

    public Task Save()
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

        if (!(await _userManager.IsInRoleAsync(user, "Administrador de Colegio")))
        {
            if (await ExistDoc(user))
            {
                throw new BadHttpRequestException($"Ya existe el user con el CI {user.Documento}");
            }
        }

        await _userManager.CreateAsync(user, password);

        return await _userManager.Users
            .Where(p => !p.Deleted && p.Email.Equals(user.Email))
            .FirstAsync();
    }

    public async Task<Persona> EditProfile(Persona p)
    {
        var personaDb = await
             _userManager.Users
             .FirstOrDefaultAsync(o => !o.Deleted && o.Id.Equals(p.Id));

        if (personaDb == null)
        {
            throw new FileNotFoundException();
        }

        if(! await _userManager.IsInRoleAsync(p, "Administrador de Colegio"))
        {
            var existOtherMail = await _userManager.Users
                .Where(x => !x.Deleted)
                .Where(x => !x.Id.Equals(p.Id))
                .Where(x => x.Documento.Equals(p.Documento) && x.DocumentoTipo.Equals(p.DocumentoTipo))
                .CountAsync() > 0;

                if(existOtherMail)
                {
                    throw new BadHttpRequestException($"El user con el Id {p.Id} ya existe");
                }
        }

        
        await _userManager.UpdateAsync(p);
        var query = await _userManager.Users
            .Where(x => !x.Deleted && x.Email.Equals(p.Email))
            .FirstAsync();
        return query;
    }

    public async Task<bool> DeleteUser(string id)
    {
        var query = await _userManager.FindByIdAsync(id);

        if (query == null || query.Deleted)
        {
            return false;
        }

        query.UserName = $"deleted.{query.Id}.{query.UserName}";
        query.Email = query.UserName;
        query.NormalizedUserName = query.UserName.ToUpper();
        query.Deleted = true;

        await _userManager.UpdateAsync(query);
        return true;
    }

    public async Task<IEnumerable<Persona>> FilterByRol(int page, int cantPorPag, string rol)
    {
        var query = await _userManager.GetUsersInRoleAsync(rol);

        return query.Where(p => !p.Deleted).OrderByDescending(i => i.Created)
            .Skip(page * cantPorPag)
            .Take(cantPorPag).ToList();
    }

    public async Task<bool> ChangePassword(Persona p, string newPassword)
    {
        await _userManager.RemovePasswordAsync(p);
        Console.WriteLine(newPassword);
        await _userManager.AddPasswordAsync(p, newPassword);
        Console.WriteLine(newPassword);
        return true;
    }


    public async Task<bool> ExistMail(string email)
    {
        var query = await _userManager.Users
            .Where(p => !p.Deleted && p.Email.Equals(email))
            .FirstOrDefaultAsync();

        return query is { Deleted: false };
    }




    public void Dispose()
    {
        _userManager.Dispose();
    }

    public async Task<IEnumerable<Persona>> GetAllByRol(string roleName, int page, int cantPorPag)
    {

        return _userManager.GetUsersInRoleAsync(roleName).Result
            .Where(p => !p.Deleted)
            .Skip(page * cantPorPag)
            .Take(cantPorPag).ToList();
    }

    public async Task<int> CountByRol(string rol)
    {
        var query = await _userManager.GetUsersInRoleAsync(rol);

        return query
            .Count(p => !p.Deleted);
    }

    public async Task<IEnumerable<Persona>> GetAllByRol(string roleName)
    {
        var query = await _userManager.GetUsersInRoleAsync(roleName);

        return query.Where(p => !p.Deleted).ToList();
    }

    public async Task<bool> ExistDoc(Persona persona)
    {
        return await _userManager.Users
            .Where(p => !p.Deleted)
            .AnyAsync(p => p.DocumentoTipo != null
                           && persona.Documento != null
                           && persona.Documento.Equals(p.Documento)
                           && p.DocumentoTipo.Equals(p.DocumentoTipo));
    }

    public async Task<Persona> FindByEmail(string email = "")
    {
        return await _userManager
                .Users
                .Include(a => a.Colegio)
                .FirstOrDefaultAsync(u => !u.Deleted && email.Equals(u.Email));
    }


    public async Task<IList<string>> GetRolesPersona(Persona user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<Persona> FindByIdAndRole(string id, string role)
    {
        var query = await _userManager.Users
            .Where(p => !p.Deleted && p.Id.Equals(id))
            .FirstOrDefaultAsync();

        if (null == query || !(await _userManager.IsInRoleAsync(query, role)))
        {
            throw new FileNotFoundException();
        }

        return query;



    }

    public async Task<bool> DeleteByUserAndRole(string id, string role)
    {
        var query = await _userManager.Users
            .Where(p => !p.Deleted && p.Id.Equals(id))
            .FirstOrDefaultAsync();


        if (query == null || (!(await _userManager.IsInRoleAsync(query, role))))
        {
            return false;
        }
        query.Modified = DateTime.Now;
        query.Deleted = true;
        query.Email = $"deleted.{query.Id}.{query.Email}";
        query.UserName = query.Email;

        await _userManager.UpdateAsync(query);


        return true;
    }

    public async Task<Persona?> FindByDocumentoAndRole(string documento = "", string role = "")
    {
        var persona = await _userManager.Users.Where(a => !a.Deleted
                && a.Documento != null
                && documento.Equals(a.Documento)).FirstOrDefaultAsync();
        if (persona != null)
        {
            var tieneRol = await _userManager.IsInRoleAsync(persona, role);
            if (tieneRol)
            {
                return persona;
            }
        }

        return null;
    }
}