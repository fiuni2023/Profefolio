namespace profefolio.Models.DTOs.Evento;
using Newtonsoft.Json;

public class EventoDTO
{
     public string? Tipo { get; set; }
     public string? Etapa { get; set; }
     public DateTime Fecha { get; set; }
     public int MateriaId{ get; set; }
     public int ClaseId { get; set; }
     public int ColegioId { get; set; }
     public string? ProfesorId { get; set; }
    
}