using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IAsistencia : IRepository<Asistencia>
    {
        Task<List<IGrouping<int, Asistencia>>> FindAll(int idMateriaLista, string userEmail);
    }
}