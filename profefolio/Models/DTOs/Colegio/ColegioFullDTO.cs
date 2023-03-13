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

    public string? Apellido
    {
        get;
        set;
    }

    public DateTime Nacimiento
    {
        get;
        set;
    }

    public string? Documento
    {
        get;
        set;
    }

    public string? DocumentoTipo
    {
        get;
        set;
    }

    public string? Genero
    {
        get;
        set;
    }

    public string? Direccion
    {
        get;
        set;
    }

    public string? Telefono
    {
        get;
        set;
    }

}