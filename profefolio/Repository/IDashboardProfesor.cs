using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IDashboardProfesor : IRepository<ColegioProfesor> 
    {
        Task<IEnumerable<Clase>> GetClasesForCardClases(int idColegio, string emailProfesor, int anho);
    }
}