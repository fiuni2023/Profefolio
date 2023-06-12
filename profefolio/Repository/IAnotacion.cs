using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IAnotacion : IRepository<Anotacion>
    {
        Task<IEnumerable<Anotacion>> GetAll();
        Task<IEnumerable<Anotacion>> GetAll(int idMateriaLista, string emailProfesor);
        Task<bool> VerificacionPreguardado(int idMateriaLista, string emailProfesor, string tituloNuevo);
        Task<bool> VerificarProfesor(int idAnotacion, string emailProfesor);
    }
}