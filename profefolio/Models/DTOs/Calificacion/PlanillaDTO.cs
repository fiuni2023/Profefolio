namespace profefolio.Models.DTOs.Calificacion;

// ReSharper disable once InconsistentNaming
public class PlanillaDTO
{
    public int MateriaId { get; set; }
    public string? Materia { get; set; }
    private List<AlumnoWithPuntajesDTO>? Alumno { get; set; }
}