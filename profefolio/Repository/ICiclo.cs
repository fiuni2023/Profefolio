using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface ICiclo : IRepository<Ciclo>
    {
        Task<IEnumerable<Ciclo>> GetAll();
    }
}