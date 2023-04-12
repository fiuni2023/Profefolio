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
        Task<ColegioProfesorResultOfCreatedDTO> Add(Persona p, string password, string rol, int idColegio);
        
    }
}