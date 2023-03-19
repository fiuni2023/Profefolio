namespace profefolio.Models.DTOs.Materia;
public class MateriaResultDTO : DataDTO
{
   /* public Materia()
        {
            detalles_materias = new HashSet<Detalles_Materia>();
        }*/
    public string? Nombre_Materia{get;set;}
    public bool Estado{get; set; }
    //public ICollection<Detalles_Materia> detalles_materias { get; set; }
}