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
        Task<bool> Exist(string idAlumno, int idColegio);
        //Task<ColegiosAlumnos> FindById(int id);
        Task<IEnumerable<ColegiosAlumnos>> FindAllByIdColegio(int page, int cantPorPag, int idColegio);
    }
}