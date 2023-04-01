namespace profefolio.Models.Entities;

public class Persona : Data
{
    public string? Nombre
    {
        get;
        set;
    }

    public string? Apellido
    {
        get;
        set;
    }

    public int Edad
    {
        get;
        set;
    }

    //Para la relacion muchos a muchos
    public IEnumerable<ColegiosAlumnos> ColegiosAlumnos { get; set; }
}