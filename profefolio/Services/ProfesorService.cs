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

        public async void Dispose()
        {
            await _context.DisposeAsync();
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

        public async Task<(ColegioProfesorResultOfCreatedDTO? resultado, Exception? ex)> Add(Persona profesor, string password, string rol, int idColegio)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                // crear el usuario
                await _userManager.CreateAsync(profesor, password);

                var prof = await _userManager.Users
                    .Where(p => !p.Deleted && p.Email.Equals(profesor.Email)).FirstOrDefaultAsync();
                if (prof == null)
                {
                    await transaction.RollbackAsync();
                    var ex = new HttpRequestException("No se pudo crear el usuario");
                    return (null, ex);
                }

                //asignar rol al usuario creado
                var rolResult = await _userManager.AddToRoleAsync(prof, rol);
                if (rolResult == null || (rolResult.Errors.Any()))
                {
                    await transaction.RollbackAsync();
                    var ex = new HttpRequestException("No se pudo crear el usuario porque no se pudo asignar el rol");
                    return (null, ex);
                }

                // asignar el profesor al colegio indicado
                var colProfesor = new ColegioProfesor()
                {
                    ColegioId = idColegio,
                    PersonaId = prof.Id,
                    Deleted = false,
                    Created = prof.Created,
                    CreatedBy = prof.CreatedBy
                };

                _context.Entry(colProfesor).State = EntityState.Added;
                var colProfResult = await Task.FromResult(colProfesor);

                await _context.SaveChangesAsync();


                if (colProfResult == null)
                {
                    await transaction.RollbackAsync();
                    var ex = new HttpRequestException("No se pudo asignar el profesor al colegio");
                    return (null, ex);
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

                return (result, null);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"{e}");
                var ex = new Exception($"{e}");
                return (null, ex);
            }

        }

        public async Task<List<Persona>> FindAllProfesoresOfColegio(int idColegio)
        {
            return await _context.Users
                        .Where(p => !p.Deleted
                            && p.ColegiosProfesor
                                .Any(pr => !pr.Deleted && pr.ColegioId == idColegio))
                        .ToListAsync();
        }
        public async Task<String?> GetProfesorIdByEmail(string userEmail)
        {
            var profesor = await _context.Users
                .FirstOrDefaultAsync(p => p.Email == userEmail);
            return profesor?.Id;
        }
        public async Task<int> GetColegioIdByProfesorId(string idProfesor) {
            var colegio = await _context.ColegiosProfesors
                .FirstOrDefaultAsync(p => p.PersonaId == idProfesor);
            return colegio.Id;
        }
        public async Task<bool> IsProfesorInMateria(int idMateriaLista, string emailProfesor)
        {
            return await _context
                    .MateriaListas
                        .Include(a => a.Profesor)
                        .AnyAsync(a => !a.Deleted && a.Id == idMateriaLista && a.Profesor.Email.Equals(emailProfesor));
        }
        public async Task<Persona> GetProfesorByEmail(string userEmail)
        {
            var profesor = await _context.Users
                .FirstOrDefaultAsync(p => p.Email == userEmail);
            return profesor;
        }
    }
}