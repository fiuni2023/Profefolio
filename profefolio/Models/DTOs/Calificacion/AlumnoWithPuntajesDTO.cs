namespace profefolio.Models.DTOs.Calificacion;

// ReSharper disable once InconsistentNaming
public class AlumnoWithPuntajesDTO
{
    public int AlumnoId { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Doc { get; set; }
    // ReSharper disable once CollectionNeverQueried.Global
    public List<EtapaDTO>? Etapas { get; set; }
}