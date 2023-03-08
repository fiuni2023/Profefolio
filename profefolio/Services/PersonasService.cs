﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using profefolio.Models;
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

        await _userManager.CreateAsync(user, password);

        return await _userManager.Users
            .Where(p => !p.Deleted && p.Email.Equals(user.Email))
            .FirstAsync();
    }

    public async Task<Persona> EditProfile(Persona old, Persona personaNew, string newPassword)
    {

        if (!old.Email.Equals(personaNew.Email) && await ExistMail(personaNew.Email))
        {
            throw new BadHttpRequestException($"El email que desea actualizar ya existe");
        }
        await _userManager.UpdateAsync(old);
        await _userManager.RemovePasswordAsync(old);
        await _userManager.CreateAsync(personaNew, newPassword);

        return await _userManager.FindByEmailAsync(personaNew.Email);
    }

    public async Task<bool> DeleteUser(string id)
    {
        var query = await _userManager.FindByIdAsync(id);

        if (query.Deleted)
        {
            return false;
        }

        query.Deleted = true;

        await _userManager.UpdateAsync(query);
        return true;
    }

    public async Task<bool> ChangePassword(Persona personaOld, Persona personaNew, string newPassoword)
    {

        await _userManager.RemovePasswordAsync(personaOld);
        await _userManager.UpdateAsync(personaOld);
        await _userManager.CreateAsync(personaNew, newPassoword);
        return true;
    }

    public async Task<bool> ExistMail(string email)
    {
        var query = await _userManager.FindByEmailAsync(email);

        return query is { Deleted: true };
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
}