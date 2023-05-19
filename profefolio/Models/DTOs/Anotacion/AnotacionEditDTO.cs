using System.ComponentModel.DataAnnotations;

namespace profefolio.Models.DTOs.Anotacion;

public class AnotacionEditDTO:DataDTO
{   
    public string Titulo { get; set; }
    public string Cuerpo { get; set; }
    
}