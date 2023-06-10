namespace profefolio.Models.DTOs.Calificacion;

// ReSharper disable once InconsistentNaming
public class EtapaDTO
{
    public string? Etapa { get; set; }
    public List<PuntajeDTO>? Puntajes { get; set; }
    public double PorcentajeTotalLogrado { get; set; }
    public double PuntajeTotalLogrado { get; set; }
}