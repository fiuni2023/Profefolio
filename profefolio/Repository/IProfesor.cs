using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IProfesor : IRepository<Persona>
    {
        Task<Persona> Save(Persona p, string password, string rol, int idColegio);
        
    }
}