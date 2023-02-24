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

    public DateOnly Edad
    {
        get;
        set;
    }
}