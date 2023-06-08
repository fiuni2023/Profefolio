using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IAnotacionAlumno : IRepository<AnotacionAlumno>
    {
        Task<List<AnotacionAlumno>> GetAllByAlumnoIdAndMateriaListaId(int idAlumno, int idMateriaLista, string profesorEmail);
        Task<bool> ValidarDatos(int idMateriaLista, string emailProfesor, int idAlumno);
    }
}