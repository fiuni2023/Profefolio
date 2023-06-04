using profefolio.Models.DTOs.Calificacion;

namespace profefolio.Repository;

public interface ICalificacion : IDisposable
{ 
    PlanillaDTO GetAll(int idMateriaLista, string user);
}