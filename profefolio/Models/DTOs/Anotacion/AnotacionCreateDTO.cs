using System.ComponentModel.DataAnnotations;

namespace profefolio.Models.DTOs.Anotacion;

public class AnotacionCreateDTO
{   
    public string Titulo { get; set; }
    public string Cuerpo { get; set; }
    public int MateriaId { get; set; }
}