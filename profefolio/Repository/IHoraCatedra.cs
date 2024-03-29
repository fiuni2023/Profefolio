using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IHoraCatedra : IRepository<HoraCatedra>
    {
        Task<List<HoraCatedra>> FindAll();
        Task<bool> Exist(string inicio = "", string fin = "");
        Task<bool> Exist(int id);
    }
}