using System.ComponentModel.DataAnnotations;

namespace profefolio.Models.DTOs.Auth;

public class Login
{
    [Required(ErrorMessage = "El Email es requerido")]
    [EmailAddress(ErrorMessage = "Debe ser de formato email")]
    public string? Email
    {
        get;
        set;
    }

    [Required(ErrorMessage = "La contraseña es requerida")]
    public string? Password
    {
        get;
        set;
    }
}