namespace profefolio.Models.Entities;

public class Calificacion : Data
{
    public double PuntajeTotal { get; set; }
    public ICollection<EventoAlumno> EventosAlumnos { get; set; }
    public double PorcentajeTotal { get; set; }
    public double PorcentajeLogrado { get; set; }
    public double PuntajeLogrado { get; set; }
}