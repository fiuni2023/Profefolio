using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        public async Task<MateriaLista> Add(MateriaLista t)
        {
            var saved = await _db.MateriaListas.AddAsync(t);

            return saved.Entity;
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public MateriaLista Edit(MateriaLista t)
        {
            _db.Entry(t).State = EntityState.Modified;
            return t;
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public async Task<MateriaLista> FindById(int id)
        {
            var query = await _db.MateriaListas
                .FirstOrDefaultAsync(p => p.Id == id && !p.Deleted);

            if (query == null) throw new FileNotFoundException();

            return query;
        }

        public IEnumerable<MateriaLista> GetAll(int page, int cantPorPag)
        {
            return _db.MateriaListas
                .Include(p => p.Clase)
                .Include(p => p.Profesor)
                .Include(p => p.Materia);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
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

        public async Task<MateriaLista> Find(int idClase, string idProfesor, int idMateria, string userLogged)
        {
            var colegio = _db.Colegios
                .FirstOrDefault(c => c.personas.UserName.Equals(userLogged));


            if (colegio == null) throw new FileNotFoundException();

            var clase = await _db.Clases
                .Include(c => c.MateriaListas)
                .Where(c => !c.Deleted)
                .Where(c => c.ColegioId == colegio.Id)
                .Where(c => c.Id == idClase)
                .FirstOrDefaultAsync();

            if (clase == null || clase.MateriaListas == null)
            {
                throw new FileNotFoundException();
            }


            var materiaListas = clase.MateriaListas
                .Where(p => p.ClaseId == idClase)
                .Where(p => p.MateriaId == idMateria)
                .Where(p => p.ProfesorId == idProfesor)
                .FirstOrDefault();

            return materiaListas;


        }

        public async Task<ClaseDetallesDTO> FindByIdClase(int idClase, string user)
        {
            var colegio = await _db.Colegios
                .Include(c => c.personas)
                .Where(c => !c.Deleted)
                .Where(c => c.personas.Email.Equals(user))
                .FirstOrDefaultAsync();

            if (colegio == null)
            {
                throw new BadHttpRequestException("Accion no valida");
            }
            var clase = await _db.Clases
                .Include(c => c.MateriaListas)
                .Where(c => !c.Deleted)
                .Where(c => c.Id == idClase)
                .FirstOrDefaultAsync();

            if (clase == null || clase.Nombre == null|| (colegio.Id != clase.ColegioId) || clase.MateriaListas == null)
            {
                throw new FileNotFoundException();
            }

            var materiaListasQuery = _db.MateriaListas;

            var materias = _db.Materias
                .Include(x => x.MateriaListas)
                .Where(x => !x.Deleted)
                .Where(x => materiaListasQuery.Any(y => y.ClaseId == clase.Id));
                
            
            var result = new ClaseDetallesDTO();

            result.ClaseId = clase.Id;
            result.NombreClase = clase.Nombre;

            var materiaProfesoresList = new List<MateriaProfesoresDTO>();

            foreach (var item in materias)
            {
                var materiaLista = item.MateriaListas;

                var materiaProfesores = new MateriaProfesoresDTO();

                materiaProfesores.IdMateria = item.Id;

                var profesorSimpleList = new List<ProfesorSimpleDTO>();

                foreach (var itemLista in materiaLista)
                {
                    var profesor = await _db.Users
                        .FirstOrDefaultAsync(x => x.Id.Equals(itemLista.ProfesorId));


                    var profeSimple = new ProfesorSimpleDTO();

                    profeSimple.Apellido = profesor.Apellido;
                    profeSimple.IdProfesor = profesor.Id;
                    profeSimple.Nombre = profesor.Apellido;

                    profesorSimpleList.Add(profeSimple);
                }

                materiaProfesores.Profesores = profesorSimpleList;

                materiaProfesoresList.Add(materiaProfesores);

            }

            result.MateriaProfesores = materiaProfesoresList;
            
            return result;
        }

        public async Task<bool> DeleteByIdClase(int idClase, string user)
        {
            try
            {
                var colegio = await _db.Colegios
                .Include(c => c.personas)
                .Where(c => !c.Deleted)
                .Where(c => c.personas.Email.Equals(user))
                .FirstOrDefaultAsync();

                if (colegio == null)
                {
                    throw new BadHttpRequestException("Accion no valida");
                }
                var clase = await _db.Clases
                    .Include(c => c.MateriaListas)
                    .Where(c => !c.Deleted)
                    .Where(c => c.Id == idClase)
                    .FirstOrDefaultAsync();

                if (clase == null || (colegio.Id != clase.ColegioId) || clase.MateriaListas == null)
                {
                    throw new FileNotFoundException();
                }

                foreach (var item in clase.MateriaListas)
                {

                    _db.MateriaListas.Remove(item);

                }

                await _db.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }


        }

        public async Task<bool> SaveMateriaLista(ClaseMateriaCreateDTO dto, string user)
        {
            var materia = await _db.Materias
                .FirstOrDefaultAsync(c => !c.Deleted && dto.IdMateria == c.Id);


            if (materia == null)
            {
                throw new BadHttpRequestException("Materia no valida");
            }

            var clase = await _db.Clases
                .FirstOrDefaultAsync(c => !c.Deleted && c.Id == dto.IdClase);

            if (clase == null)
            {
                throw new BadHttpRequestException("clase no valida");
            }

            var colegio = await _db.Colegios
                .Include(c => c.personas)
                .Where(c => !c.Deleted)
                .Where(c => c.personas != null)
                .Where(c => !c.personas.Deleted)
                .Where(c => c.personas.Email != null)
                .Where(c => c.personas.Email.Equals(user))
                .FirstOrDefaultAsync();

            if (colegio == null || colegio.Id != clase.ColegioId)
            {
                throw new BadHttpRequestException("No estas autorizado a modificar una clase, que no pertenece a tu colegio");
            }

            var distinct = dto.IdProfesores.Distinct();

            foreach (var item in distinct)
            {
                var p = await _db.Users
                    .FirstOrDefaultAsync(x => !x.Deleted && x.Id.Equals(item));

                if (p == null)
                {
                    throw new BadHttpRequestException("Profesor no valido");
                }

                var rol = await _db.Roles
                    .FirstOrDefaultAsync(r => r.Name.Equals("Profesor"));

                bool esProfesor = await _db.UserRoles
                    .Where(ur => ur.UserId.Equals(p.Id) && ur.RoleId.Equals(rol.Id))
                    .CountAsync() > 0;

                if (!(esProfesor))
                {
                    throw new BadHttpRequestException("Profesor no valido");
                }

                await _db.MateriaListas.AddAsync(new MateriaLista
                {
                    ClaseId = dto.IdClase,
                    ProfesorId = item,
                    MateriaId = dto.IdMateria,
                    Created = DateTime.Now,
                    CreatedBy = user
                });
                try
                {
                    await this.Save();
                }
                catch (Exception e)
                {
                    return false;
                }


            }
            return true;
        }

        public async Task<bool> EditMateriaLista(ClaseMateriaEditDTO dto, string user)
        {
            var materia = await _db.Materias
                .FirstOrDefaultAsync(c => !c.Deleted && dto.IdMateria == c.Id);


            if (materia == null)
            {
                throw new BadHttpRequestException("Materia no valida");
            }

            var clase = await _db.Clases
                .FirstOrDefaultAsync(c => !c.Deleted && c.Id == dto.IdClase);

            if (clase == null)
            {
                throw new BadHttpRequestException("clase no valida");
            }

            var colegio = await _db.Colegios
                .Include(c => c.personas)
                .Where(c => !c.Deleted)
                .Where(c => c.personas != null)
                .Where(c => !c.personas.Deleted)
                .Where(c => c.personas.Email != null)
                .Where(c => c.personas.Email.Equals(user))
                .FirstOrDefaultAsync();

            if (colegio == null || colegio.Id != clase.ColegioId)
            {
                throw new BadHttpRequestException("Accion no valida");
            }

            foreach (var item in dto.IdProfesores.Distinct())
            {
                var p = await _db.Users
                    .FirstOrDefaultAsync(x => !x.Deleted && x.Id.Equals(item));

                if (p == null)
                {
                    throw new BadHttpRequestException("Profesor no valido");
                }

                var rol = await _db.Roles
                    .FirstOrDefaultAsync(r => r.Name.Equals("Profesor"));

                bool esProfesor = await _db.UserRoles
                    .Where(ur => ur.UserId.Equals(p.Id) && ur.RoleId.Equals(rol.Id))
                    .CountAsync() > 0;

                if (!(esProfesor))
                {
                    throw new BadHttpRequestException("Profesor no valido");
                }
                var listaDetalle = await Find(dto.IdClase, item, dto.IdMateria, user);

                if (listaDetalle == null)
                {
                    await _db.MateriaListas.AddAsync(new MateriaLista
                    {
                        ClaseId = dto.IdClase,
                        ProfesorId = item,
                        MateriaId = dto.IdMateria,
                        Created = DateTime.Now,
                        CreatedBy = user
                    });
                }
                else
                {
                    listaDetalle.ClaseId = dto.IdClase;
                    listaDetalle.MateriaId = dto.IdMateria;
                    listaDetalle.ProfesorId = item;
                    listaDetalle.ModifiedBy = "user";
                    listaDetalle.Modified = DateTime.Now;
                }

                try
                {
                    await this.Save();
                }
                catch (Exception e)
                {
                    return false;
                }


            }
            return true;
        }

        public async Task<List<MateriaLista>> FindByIdClaseAndUser(int idClase, string userEmail = "", string role = "")
        {

            if ("Administrador de Colegio".Equals(role))
            {
                var colegio = await _db.Colegios.FirstOrDefaultAsync(a => !a.Deleted && userEmail.Equals(a.personas.Email));
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
    }
}
