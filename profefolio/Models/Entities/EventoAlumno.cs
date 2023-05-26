using System.Diagnostics.CodeAnalysis;

namespace profefolio.Models.Entities;

public class EventoAlumno : Data
{
    public Evento Evento { get; set; }
    public Persona Alumno { get; set; }
    public double PuntajeLogrado { get; set; }
    public double PorcentajeLogrado { get; set; }
    public Calificacion Calificacion { get; set; }
    public int IdCalificacion { get; set; }
}