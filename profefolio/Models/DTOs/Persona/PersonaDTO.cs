namespace profefolio.Models.DTOs.Persona;
// ReSharper disable once InconsistentNaming
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