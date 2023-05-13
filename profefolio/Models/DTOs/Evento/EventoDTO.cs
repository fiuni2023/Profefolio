namespace profefolio.Models.DTOs.Evento;
using Newtonsoft.Json;

public class EventoDTO
{
     public string? Tipo { get; set; }
     public DateTime Fecha { get; set; }
     public int MateriaId{ get; set; }
     public int ClaseId { get; set; }
     public int ColegioId { get; set; }
     
     [JsonIgnore] // Agrega el atributo JsonIgnore al campo ProfesorId
     public String? ProfesorId { get; set; }
    
}