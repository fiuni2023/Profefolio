namespace profefolio.Models.DTOs.Calificacion;

// ReSharper disable once InconsistentNaming
public class PuntajeDTO
{
    public double PuntajeTotal { get; set; }
    public double PuntajeLogrado { get; set; }
    public double PorcentajeLogrado { get; set; }
    public int IdEvaluacion { get; set; }
    public string? NombreEvaluacion { get; set; }
    public string? Tipo { get; set; }
}