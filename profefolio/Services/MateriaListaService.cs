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
            var colegio = await _db.Colegios
                .Include(c => c.personas)
                .Where(c => !c.Deleted)
                .Where(c => c.personas.Email.Equals(user))
                .FirstOrDefaultAsync();

            if (colegio == null)
            {
                throw new BadHttpRequestException("Accion no valida");
            }

            var profesores = _db.Users.ToList();

            var clase = await _db.Clases
                .Include(c => c.MateriaListas)
                .Where(c => !c.Deleted)
                .Where(c => c.Id == idClase)
                .FirstOrDefaultAsync();

            if (clase == null || clase.Nombre == null|| (colegio.Id != clase.ColegioId) || clase.MateriaListas == null)
            {
                throw new FileNotFoundException();
            }

            

            var materias = _db.Materias
                .Include(x => x.MateriaListas)
                .Where(x => !x.Deleted)
                .Where(x => x.MateriaListas.Any(y => y.ClaseId == clase.Id));
                
            
            var result = new ClaseDetallesDTO();

            result.ClaseId = clase.Id;
            result.NombreClase = clase.Nombre;

            var materiaProfesoresList = new List<MateriaProfesoresDTO>();

            foreach (var item in materias)
            {
                var materiaLista = item.MateriaListas;

                var materiaProfesores = new MateriaProfesoresDTO();

                materiaProfesores.IdMateria = item.Id;
                materiaProfesores.Materia = item.Nombre_Materia;

                var profesorSimpleList = new List<ProfesorSimpleDTO>();

                foreach (var itemLista in materiaLista)
                {
                    var profesor = profesores.Find(x => x.Id.Equals(itemLista.ProfesorId));


                    var profeSimple = new ProfesorSimpleDTO();

                    profeSimple.Apellido = profesor.Apellido;
                    profeSimple.IdProfesor = profesor.Id;
                    profeSimple.Nombre = profesor.Nombre;

                    profesorSimpleList.Add(profeSimple);
                }

                materiaProfesores.Profesores = profesorSimpleList;

                materiaProfesoresList.Add(materiaProfesores);

            }

            result.MateriaProfesores = materiaProfesoresList;
            
            return result;
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

        public async Task<bool> Put(string email, MateriaListaPutDTO dto)
        {
            var user =  await _db.Users
                .Where(u => !u.Deleted)
                .Where(u => u.Email.Equals(email))
                .FirstOrDefaultAsync();

            if(user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var colegio = await _db.Colegios
                .Include(c => c.personas)
                .Where(c => !c.Deleted)
                .Where(c => c.personas != null && c.personas.Id.Equals(user.Id))
                .FirstOrDefaultAsync();

            
            if(colegio == null)
            {
                throw new UnauthorizedAccessException();
            }

            var clase = await _db.Clases
                .Where(c => !c.Deleted)
                .Where(c => c.Id == dto.IdClase)
                .Where(c => c.ColegioId == colegio.Id)
                .FirstOrDefaultAsync();

            if(clase == null)
            {
                throw new UnauthorizedAccessException();
            }

            


            throw new NotImplementedException();
        }
    }
}
