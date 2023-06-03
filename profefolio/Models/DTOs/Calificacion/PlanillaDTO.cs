namespace profefolio.Models.DTOs.Calificacion;

public class PlanillaDTO
{
    public int MateriaId { get; set; }
    public string? Materia { get; set; }
    public List<EtapaDTO>? Etapas { get; set; }
}