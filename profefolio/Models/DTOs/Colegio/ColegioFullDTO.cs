//con atributos de administrador
namespace profefolio.Models.DTOs.Colegio;

public class ColegioFullDTO : DataDTO
{
    public string? Nombre
    {
        get;
        set;
    }

    public Boolean Estado
    {
        get;
        set;
    }
    public string? NombreAdministrador
    {
        get;
        set;
    }

    public string? ApellidoAdministrador
    {
        get;
        set;
    }

}