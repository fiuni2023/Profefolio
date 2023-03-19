using System.ComponentModel.DataAnnotations;

namespace profefolio.Models.DTOs.Alumno;

public class AlumnoCreateDTO
{
    [Required(ErrorMessage = "El nombre es requerido")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El apellido es requerido")]
    public string Apellido { get; set; }
    
    [Required(ErrorMessage = "El Nacimiento es requerido")]
    public DateTime Nacimiento { get; set; }

    [Required(ErrorMessage = "El Documento es requerido")]
    public string Documento { get; set; }

    [Required(ErrorMessage = "El Tipo de Documento es requerido")]
    public string DocumentoTipo { get; set; }

    [Required(ErrorMessage = "El genero es requerido")]
    public string Genero { get; set; }
    
    public string Direccion { get; set; }
    
}