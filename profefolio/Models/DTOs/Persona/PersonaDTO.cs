namespace profefolio.Models.DTOs.Persona;

public class PersonaDTO : DataDTO
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