namespace profefolio.Models.DTOs.Materia;
using Newtonsoft.Json;
public class MateriaResultFullDTO : DataDTO
{
   
    public string? Nombre_Materia{get;set;}
    public int MateriaListaId{get;set;}
    [JsonIgnore]
    public int MateriaId{get;set;}
}