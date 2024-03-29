using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.DTOs.ColegioProfesor;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IProfesor : IRepository<Persona>
    {
        Task<(ColegioProfesorResultOfCreatedDTO? resultado, Exception? ex)> Add(Persona p, string password, string rol, int idColegio);
        Task<List<Persona>> FindAllProfesoresOfColegio(int idColegio);
        Task<(List<Persona>, int)> FindAllProfesoresOfColegioPage(int page, int cantPorPag, string adminEmail, int idColegio);

        Task<bool> IsProfesorInMateria(int idMateriaLista, string emailProfesor);
        Task<String?> GetProfesorIdByEmail(string userEmail);
        Task<Persona> GetProfesorByEmail(string userEmail);
        Task<int> GetColegioIdByProfesorId(string idProfesor);
    }
}