using System.ComponentModel.DataAnnotations;

namespace profefolio.Models.DTOs.Auth;

public class ModelPassword
{
    [DataType(DataType.Password)]
    [Display(Name = "Confirmar la nueva contraseña")]
    [Required(ErrorMessage = "La contraseña es requerida")]
    
    public string NewPassword
    {
        get;
        set;
    }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "")]
    [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la contraseña de confirmación no coinciden.")]
    public string ConfirmPasswrord
    {
        get;
        set;
    }
}