namespace profefolio.Models.DTOs.Evento;

public class EventoDTO
{
     public string? Tipo { get; set; }
     public DateTime Fecha { get; set; }
     public int MateriaId{ get; set; }
     public int ClaseId { get; set; }
     public int ColegioId { get; set; }
    
}