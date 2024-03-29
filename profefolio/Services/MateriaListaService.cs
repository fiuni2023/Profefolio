﻿using Microsoft.EntityFrameworkCore;
using profefolio.Models;
using profefolio.Models.DTOs.ClaseMateria;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services
{
    public class MateriaListaService : IMateriaLista
    {
        private bool disposedValue;
        private readonly ApplicationDbContext _db;

        public MateriaListaService(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<MateriaLista> GetAll(int page, int cantPorPag)
        {
            return _db.MateriaListas
                .Include(p => p.Clase)
                .Include(p => p.Profesor)
                .Include(p => p.Materia);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _db.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


        public async Task<bool> IsUsedMateria(int idMateria)
        {
            return await _db.MateriaListas.AnyAsync(d => d.MateriaId == idMateria);
        }


        public async Task<ClaseDetallesDTO> FindByIdClase(int idClase, string user)
        {
            var userL = await _db.Users
                .Where(u => !u.Deleted && u.Email.Equals(user))
                .FirstAsync();


            var colegio = await _db.Colegios
                .Where(c => c.PersonaId != null && !c.Deleted && c.PersonaId.Equals(userL.Id))
                .FirstAsync();

            if (colegio == null)
            {
                throw new BadHttpRequestException("Accion no valida");
            }

            var profesores = _db.Users
                .Where(p => !p.Deleted)
                .ToList();

            var clase = await _db.Clases
                .Include(c => c.MateriaListas)
                .Where(c => !c.Deleted && c.Id == idClase)
                .FirstOrDefaultAsync();


            if (clase?.Nombre == null || (colegio.Id != clase.ColegioId) || clase.MateriaListas == null)
            {
                throw new FileNotFoundException();
            }


            var materias = _db.Materias
                .Include(x => x.MateriaListas)
                .Where(x => !x.Deleted)
                .Where(x => x.MateriaListas.Any(y => y.ClaseId == clase.Id))
                .ToList();


            var result = new ClaseDetallesDTO();

            result.ClaseId = clase.Id;
            result.NombreClase = clase.Nombre;

            var materiaProfesoresList = new List<MateriaProfesoresDTO>();

            foreach (var item in materias)
            {
                var materiaLista = item.MateriaListas
                    .Where(d => !d.Deleted)
                    .Where(d => d.ClaseId == idClase)
                    .ToList();

                if (!item.MateriaListas.All(x => x.Deleted))
                {
                    var materiaProfesores = new MateriaProfesoresDTO();
                    materiaProfesores.IdMateria = item.Id;
                    materiaProfesores.Materia = item.Nombre_Materia;

                    var profesorSimpleList = new List<ProfesorSimpleDTO>();

                    foreach (var itemLista in materiaLista)
                    {
                        var profesor = profesores.Find(x => x.Id.Equals(itemLista.ProfesorId));

                        var colegioProfesorVerify = _db.ColegiosProfesors
                            .Any(cp => profesor != null && cp.PersonaId != null && !cp.Deleted &&
                                       cp.PersonaId.Equals(profesor.Id)
                                       && cp.ColegioId == colegio.Id);

                        var mlVerify = itemLista.ProfesorId.Equals(profesor?.Id);
                        if (colegioProfesorVerify && mlVerify)
                        {
                            var profeSimple = new ProfesorSimpleDTO();
                            profeSimple.Apellido = profesor.Apellido;
                            profeSimple.IdProfesor = profesor.Id;
                            profeSimple.Nombre = profesor.Nombre;
                            profesorSimpleList.Add(profeSimple);
                        }
                    }

                    materiaProfesores.Profesores = profesorSimpleList;

                    materiaProfesoresList.Add(materiaProfesores);
                }
            }

            result.MateriaProfesores = materiaProfesoresList;

            return result;
        }


        public async Task<List<MateriaLista>> FindByIdClaseAndUser(int idClase, string userEmail = "", string role = "")
        {
            if ("Administrador de Colegio".Equals(role))
            {
                var colegio =
                    await _db.Colegios.FirstOrDefaultAsync(a => !a.Deleted && userEmail.Equals(a.personas.Email));
                if (colegio == null)
                {
                    throw new FileNotFoundException("No es Administrador del Colegio");
                }

                return await _db.MateriaListas
                    .Where(a => !a.Deleted
                                && a.Clase.ColegioId == colegio.Id
                                && a.ClaseId == idClase)
                    .Include(a => a.Materia)
                    .Include(a => a.Profesor)
                    .ToListAsync();
            }


            if ("Profesor".Equals(role))
            {
                return await _db.MateriaListas
                    .Where(a => !a.Deleted
                                && userEmail.Equals(a.Profesor.Email)
                                && a.ClaseId == idClase)
                    .Include(a => a.Materia)
                    .Include(a => a.Profesor)
                    .ToListAsync();
            }

            throw new BadHttpRequestException("El usuario no tienen acceso");
        }

        public async Task<bool> Put(string email, MateriaListaPutDTO dto)
        {
            var user = await _db.Users
                .Where(u => !u.Deleted)
                .Where(u => u.Email.Equals(email))
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var colegio = await _db.Colegios
                .Include(c => c.ColegioProfesores)
                .Include(c => c.personas)
                .Where(c => !c.Deleted)
                .Where(c => c.personas.Id.Equals(user.Id))
                .FirstOrDefaultAsync();


            if (colegio == null)
            {
                throw new UnauthorizedAccessException();
            }

            var clase = await _db.Clases
                .Where(c => !c.Deleted)
                .Where(c => c.Id == dto.IdClase)
                .Where(c => c.ColegioId == colegio.Id)
                .FirstOrDefaultAsync();

            if (clase == null)
            {
                throw new UnauthorizedAccessException();
            }

            if (dto.Materias.Count < 1)
            {
                throw new BadHttpRequestException("No se admiten elementos vacios");
            }

            foreach (var materia in dto.Materias)
            {
                var existMateria = _db.Materias
                    .Where(m => !m.Deleted)
                    .Any(m => m.Id == materia.IdMateria);


                if (!existMateria)
                {
                    throw new FileNotFoundException();
                }

                var profesorDist = materia.Profesores
                    .DistinctBy(x => x.IdProfesor);

                if (profesorDist.Count() != materia.Profesores.Count())
                    throw new BadHttpRequestException("No se admiten elementos repetidos");
                

                foreach (var profesor in materia.Profesores)
                {
                    //Validar los roles del profesor
                    var profe = await _db.Users.FindAsync(profesor.IdProfesor);

                    if (profe == null)
                    {
                        throw new FileNotFoundException();
                    }

                    var role = await _db.Roles
                        .FirstOrDefaultAsync(r => r.Name.Equals("Profesor"));

                    if (role == null)
                    {
                        throw new FileNotFoundException();
                    }


                    var hasRole = _db.UserRoles
                        .Any(ur => ur.RoleId.Equals(role.Id) && ur.UserId.Equals(profe.Id));


                    if (!hasRole)
                    {
                        throw new UnauthorizedAccessException();
                    }


                    //Validando si el profesor es de mi colegio

                    var existProfeEnColeg = colegio.ColegioProfesores
                        .Where(cp => !cp.Deleted)
                        .Any(cp => cp.ColegioId == colegio.Id
                                   && cp.PersonaId != null
                                   && cp.PersonaId.Equals(profesor.IdProfesor));

                    if (!existProfeEnColeg)
                    {
                        throw new BadHttpRequestException("Profesor no valido");
                    }

                    //Agregando las relaciones

                    switch (profesor.Estado)
                    {
                        case 'N':
                            var materiaLista = new MateriaLista()
                            {
                                ClaseId = dto.IdClase,
                                MateriaId = materia.IdMateria,
                                ProfesorId = profesor.IdProfesor,
                                Deleted = false,
                                CreatedBy = email,
                                Created = DateTime.Now
                            };
                            await _db.MateriaListas.AddAsync(materiaLista);
                            break;
                        case 'D':
                            var materiaListaD = await _db.MateriaListas
                                .Include(ml => ml.Horarios)
                                .Where(ml => !ml.Deleted)
                                .Where(ml => ml.ClaseId == dto.IdClase
                                             && ml.MateriaId == materia.IdMateria
                                             && ml.ProfesorId.Equals(profesor.IdProfesor))
                                .FirstOrDefaultAsync();

                            if (materiaListaD == null)
                            {
                                throw new FileNotFoundException();
                            }

                            materiaListaD.ModifiedBy = email;
                            materiaListaD.Modified = DateTime.Now;
                            materiaListaD.Deleted = true;
                            break;
                        
                        case  '-' : 
                            break;
                        default: 
                            throw new BadHttpRequestException("Comando no valido");
                    }
                }
            }


            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Persona> GetProfesorOfMateria(int idMateriaLista, string profesorEmail)
        {
            var materia = await _db.MateriaListas
                .Include(a => a.Profesor)
                .FirstOrDefaultAsync(a => !a.Deleted
                                          && a.Id == idMateriaLista
                                          && profesorEmail.Equals(a.Profesor.Email));

            if (materia == null)
            {
                throw new FileNotFoundException("No es Profesor de la Materia");
            }

            return materia.Profesor;
        }

        public async Task<MateriaLista> FindById(int id)
        {
            var query = await _db.MateriaListas
                .Include(c => c.Clase)
                .ThenInclude(col => col.Colegio)
                .Where(p => !p.Deleted && p.Id == id)
                .FirstOrDefaultAsync();

            if (query == null)
            {
                throw new FileNotFoundException();
            }

            return query;
        }

        public async Task<MateriaLista> Filter(int idClase, int idColegio, string idProfesor, int idMateria)
        {
            var query = await _db.MateriaListas
                .Include(ml => ml.Clase)
                .Where(ml => !ml.Deleted)
                .Where(ml => ml.Clase.Colegio != null
                             && idClase == ml.ClaseId
                             && ml.ProfesorId.Equals(idProfesor)
                             && ml.MateriaId == idMateria
                             && ml.Clase.Colegio.Id == idColegio)
                .FirstOrDefaultAsync();

            if (query == null)
            {
                throw new FileNotFoundException();
            }

            return query;
        }

        public async Task<List<MateriaLista>> FilterByIdClase(int idClase)
        {
            var query = await _db.MateriaListas
                .Where(d => !d.Deleted && d.ClaseId == idClase)
                .ToListAsync();

            return query;
        }
    }
}