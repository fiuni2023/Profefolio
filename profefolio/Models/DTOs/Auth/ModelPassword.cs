using System.ComponentModel.DataAnnotations;

namespace profefolio.Models.DTOs.Auth;

public class ModelPassword
{
    [Display(Name = "Confirmar la nueva contraseña")]
    [Required(ErrorMessage = "La contraseña es requerida")]
    public string NewPassword
    {
        get;
        set;
    }

    
    [Required(ErrorMessage = "necesitas confirmar la contraseña")]
    [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la contraseña de confirmación no coinciden.")]
    public string ConfirmPasswrord
    {
        get;
        set;
    }
}