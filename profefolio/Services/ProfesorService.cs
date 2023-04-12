using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using profefolio.Models;
using profefolio.Models.DTOs.ColegioProfesor;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services
{
    public class ProfesorService : IProfesor
    {
        // crear variable de UserManager y que lo reciba como parametro en el contructor
        private UserManager<Persona> _userManager;
        private ApplicationDbContext _context;

        public ProfesorService(ApplicationDbContext context, UserManager<Persona> userManager)
        {
            _userManager = userManager;
            _context = context;
        }


        public Task<Persona> Add(Persona t)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Persona Edit(Persona t)
        {
            throw new NotImplementedException();
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public Task<Persona> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Persona> GetAll(int page, int cantPorPag)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public async Task<ColegioProfesorResultOfCreatedDTO> Add(Persona p, string password, string rol, int idColegio)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {

                // crear el usuario
                await _userManager.CreateAsync(p, password);

                var prof = await _userManager.Users
                    .Where(p => !p.Deleted && p.Email.Equals(p.Email)).FirstOrDefaultAsync();
                if (prof == null)
                {
                    await transaction.RollbackAsync();
                    throw new HttpRequestException("No se pudo crear el usuario");
                }

                //asignar rol al usuario creado
                var rolResult = await _userManager.AddToRoleAsync(p, rol);
                if (rolResult == null || (rolResult.Errors.Any()))
                {
                    await transaction.RollbackAsync();
                    throw new HttpRequestException("No se pudo crear el usuario porque no se pudo asignar el rol");
                }

                // asignar el profesor al colegio indicado
                var colProfesor = new ColegioProfesor()
                {
                    ColegioId = idColegio,
                    PersonaId = prof.Id
                };
                _context.Entry(colProfesor).State = EntityState.Added;
                var colProfResult = await Task.FromResult(colProfesor);

                if (colProfResult == null)
                {
                    await transaction.RollbackAsync();
                    throw new HttpRequestException("No se pudo asignar el profesor al colegio");
                }

                await transaction.CommitAsync();

                var result = new ColegioProfesorResultOfCreatedDTO()
                {
                    Id = colProfResult.Id,
                    IdColegio = colProfResult.ColegioId,
                    IdProfesor = colProfResult.PersonaId,
                    Nombre = prof.Nombre,
                    Apellido = prof.Apellido,
                    Email = prof.Email,
                    Direccion = prof.Direccion,
                    Documento = prof.Documento,
                    DocumentoTipo = prof.DocumentoTipo,
                    Genero = prof.EsM ? "Masculino" : "Feminino",
                    Nacimiento = prof.Nacimiento,
                    Telefono = prof.PhoneNumber,
                };

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                throw new Exception($"{e}");
            }

        }
    }
}