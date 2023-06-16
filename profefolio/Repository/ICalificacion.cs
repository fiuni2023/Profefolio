using profefolio.Models.DTOs.Calificacion;

namespace profefolio.Repository;

public interface ICalificacion : IDisposable
{ 
    Task<PlanillaDTO> GetAll(int idMateriaLista, string user);
    Task<PlanillaDTO> Put(int idMateriaLista, CalificacionPutDto dto, string user);
}