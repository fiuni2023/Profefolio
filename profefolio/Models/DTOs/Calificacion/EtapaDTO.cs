namespace profefolio.Models.DTOs.Calificacion;

public class EtapaDTO
{
    public string? Etapa { get; set; }
    public List<AlumnoWithPuntajesDTO>? Alumnos { get; set; }
}