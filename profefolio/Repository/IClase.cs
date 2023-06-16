using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IClase : IRepository<Clase>
    {
        Task<int> Count(int idColegio);
        Task<IEnumerable<Clase>> GetByIdColegio(int idColegio);
        Task<IEnumerable<Clase>> GetAllByIdColegio(int page, int cantPorPag, int idColegio);
        Task<Clase> FindByIdAndUser(int idClase, string user);
        Task<(List<string>, List<int>)> GetCantidadAlumnos(string adminEmail);
    }
}