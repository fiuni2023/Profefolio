namespace profefolio.Models.DTOs.Evento;
public class EventoResultFullDTO: DataDTO
{
     public string? Tipo { get; set; }
     public DateTime Fecha { get; set; }
     public string? NombreMateria{ get; set; }
     public string? NombreClase { get; set; }
     public string? NombreColegio { get; set; }
    
}