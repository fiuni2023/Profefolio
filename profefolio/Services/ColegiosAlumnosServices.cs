using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services
{
    public class ColegiosAlumnosServices : IColegiosAlumnos
    {
        public Task<int> Count(int idColegio)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Clase>> GetAllByIdColegio(int page, int cantPorPag, int idColegio)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ColegiosAlumnos>> GetByIdColegio(int idColegio)
        {
            throw new NotImplementedException();
        }
    }
}