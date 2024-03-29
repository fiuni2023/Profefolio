namespace profefolio.Models.Entities;
using Models.DTOs.Persona;
public class Colegio : Data
{
    public string? Nombre
    {
        get;
        set;
    }
    public string? PersonaId
    {
        get;
        set;
    }
    public Persona personas { get; set; }

    //Para la relacion muchos a muchos
    public IEnumerable<ColegioProfesor> ColegioProfesores { get; set; }
    public IEnumerable<ColegiosAlumnos> ColegiosAlumnos { get; set; }
    public IEnumerable<Clase> ListaClases { get; set; }
}