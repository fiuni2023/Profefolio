using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IColegiosAlumnos : IRepository<ColegiosAlumnos>
    {
        Task<int> Count(int idColegio);
        Task<int> Count(string adminEmail);
        Task<bool> Exist(string idAlumno, int idColegio);
        //Task<ColegiosAlumnos> FindById(int id);
        Task<IEnumerable<ColegiosAlumnos>> FindAllByIdColegio(int page, int cantPorPag, int idColegio);
        Task<IEnumerable<ColegiosAlumnos>> FindAllByAdminEmail(int page, int cantPorPag, string adminEmail);
        Task<IEnumerable<ColegiosAlumnos>> FindAllNoAssignedToClaseByEmailAdminAndIdClase(string adminEmail, int idClase);
        Task<IEnumerable<Persona>> FindNotAssigned(string user, int idClase, int page, int cantPerPage);
        Task<int> CountNotAssigned(string user, int idClase);
    }
}