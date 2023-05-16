using System.ComponentModel.DataAnnotations;

namespace profefolio
{
    public class ClaseMateriaEditDTO
    {
        [Required(ErrorMessage = "Requerido")]
        public int IdClase {get; set;}
        [Required(ErrorMessage = "Requerido")]
        public int IdMateria {get; set;}
        [Required(ErrorMessage = "Requerido")]
        public List<string> IdProfesores {get; set;}
    }
}