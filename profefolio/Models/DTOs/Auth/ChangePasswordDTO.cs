using System.ComponentModel.DataAnnotations;

namespace profefolio.Models.DTOs.Auth;

public class ChangePasswordDTO
{
    [Required(ErrorMessage = "la contraseña es requerida")]
    public string? Password
    {
        get;
        set;
    }

    [Required(ErrorMessage = "necesitas confirmar la contraseña")]
    [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
    public string? ConfirmPassword
    {
        get;
        set;
    }
}