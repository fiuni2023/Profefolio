using System.ComponentModel.DataAnnotations;

namespace profefolio.Models.DTOs.Alumno;

public class AlumnoEditDTO : AlumnoCreateDTO
{
    [Required(ErrorMessage = "El Id es requerido")]
    public string Id { get; set; }
}