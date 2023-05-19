using System.ComponentModel.DataAnnotations;

namespace profefolio.Models.DTOs.Anotacion;

public class AnotacionResultDTO:DataDTO
{   
    public string Titulo { get; set; }
    public string Cuerpo { get; set; }
    
}