namespace profefolio.Models.DTOs.Calificacion;

public class EtapaDTO
{
    public string? Etapa { get; set; }
    private List<AlumnoWithPuntajesDTO>? Alumnos { get; set; }
}