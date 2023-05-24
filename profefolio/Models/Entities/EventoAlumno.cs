namespace profefolio.Models.Entities;

public class EventoAlumno : Data
{  
    public Evento Evento { get; set; }
    public Persona Alumno { get; set; }
    public double PuntajeLogrado { get; set; }
    public int IdCalificacion { get; set; }
}