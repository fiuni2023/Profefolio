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
}