using System.ComponentModel.DataAnnotations;

namespace profefolio.Models.DTOs.Calificacion;

public class CalificacionPutDto
{
    [Required(ErrorMessage = "Requerido")]
    public int IdEvaluacion { get; set; }
    [Required(ErrorMessage = "Requerido")]
    public string? Modo { get; set; }
    public double Puntaje { get; set; }
    public string? NombreEvaluacion { get; set; }
    public double PuntajeTotal { get; set; }
}