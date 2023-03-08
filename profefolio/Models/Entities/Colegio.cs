namespace profefolio.Models.Entities;

public class Colegio : Data
{
    public string? Nombre
    {
        get;
        set;
    }

    public bool Estado
    {
        get;
        set;
    }

     public int PersonaId
    {
        get;
        set;
    }
    public Persona personas { get; set; }
}