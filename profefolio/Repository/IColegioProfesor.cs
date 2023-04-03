using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IColegioProfesor : IRepository<ColegioProfesor>
    {
        Task<int> Count(int idColegio);
        Task<bool> Exist(string idProf, int idColegio);
        Task<IEnumerable<ColegioProfesor>> FindAllByIdColegio(int page, int cantPorPag, int idColegio);
    }
}