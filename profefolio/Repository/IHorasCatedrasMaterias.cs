using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IHorasCatedrasMaterias : IRepository<HorasCatedrasMaterias>
    {
        Task<List<HorasCatedrasMaterias>> GetAll();
        Task<List<HorasCatedrasMaterias>> GetAllHorariosByEmailProfesorAndYear(string emailProfesor, int anho);
        Task<bool> Exist(int idMateriaLista, int idHoraCatedra, string dia);
    }
}