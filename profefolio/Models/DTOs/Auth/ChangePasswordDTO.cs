using System.ComponentModel.DataAnnotations;

namespace profefolio.Models.DTOs.Auth;

public class ChangePasswordDTO
{
    [RegularExpression(@"^.*(?=.{8,})((?=.*[!@#$%^&*()\-_=+{};:,<.>]){1})(?=.*\d)((?=.*[a-z]){1})((?=.*[A-Z]){1}).*$", 
            ErrorMessage = "Se requiere como minimo: longitud de 8, letras mayusculas y minusculas, numero y caracter no Alfanumerico ")]
    
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