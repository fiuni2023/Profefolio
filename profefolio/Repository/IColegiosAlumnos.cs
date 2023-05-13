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
        Task<IEnumerable<ColegiosAlumnos>> FindAllByIdColegio(int page, int cantPorPag, int idColegio);
        Task<IEnumerable<ColegiosAlumnos>> FindAllByAdminEmail(int page, int cantPorPag, string adminEmail);
        Task<IEnumerable<ColegiosAlumnos>> FindAllNoAssignedToClaseByEmailAdminAndIdClase(string adminEmail, int idClase);
        Task<IEnumerable<ColegiosAlumnos>> FindNotAssigned(string user, int idClase, int page, int cantPerPage);
        Task<IEnumerable<ColegiosAlumnos>> FindNotAssignedByYear(string user, int idClase, int page, int cantPerPage);
        Task<int> CountNotAssigned(string user, int idClase);
        Task<int> ContNotAssignedByYear(int year, string user, int idClase);
        Task<IEnumerable<ColegiosAlumnos>> FindAll(string user, int page, int cantPerPage);
        Task<IEnumerable<ColegiosAlumnos>> GetNotAssignedByYear(int year, string user, int idClase);
    }
}