using System.ComponentModel.DataAnnotations;

namespace profefolio.Models.DTOs.Evento;
using Newtonsoft.Json;

// ReSharper disable once InconsistentNaming
public class EventoDTO
{
     [Required(ErrorMessage = "Requerido")]
     public string? Tipo { get; set; }
     [Required(ErrorMessage = "Requerido")]
     public string? Nombre { get; set; }
     [Required(ErrorMessage = "Requerido")]
     public string? Etapa { get; set; }
     [Required(ErrorMessage = "Requerido")]
     public DateTime Fecha { get; set; }
     [Required(ErrorMessage = "Requerido")]
     public int IdMateriaLista { get; set; }
     public double Puntaje { get; set; } = 0;

}