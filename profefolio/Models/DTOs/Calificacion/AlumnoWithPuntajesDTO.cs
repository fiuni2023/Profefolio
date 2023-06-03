namespace profefolio.Models.DTOs.Calificacion;

public class AlumnoWithPuntajesDTO
{
    public double PorcentajeTotalLogrado { get; set; }
    public double PuntajeTotalLogrado { get; set; }
    public int Alumno { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Doc { get; set; }
}