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

        if (await ExistDoc(user))
        {
            throw new BadHttpRequestException($"El usuario con doc {user.Documento} ya existe");
        }

        await _userManager.CreateAsync(user, password);

        return await _userManager.Users
            .Where(p => !p.Deleted && p.Email.Equals(user.Email))
            .FirstAsync();
    }

    public async Task<Persona> EditProfile(Persona p)
    {
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
        query.UserName = $"deleted.{query.Id}.{query.UserName}";
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

        return  _userManager.GetUsersInRoleAsync(roleName).Result
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
        return await _userManager.Users.FirstOrDefaultAsync(u => !u.Deleted && email.Equals(u.Email));
    }
}