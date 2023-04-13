using System.ComponentModel.DataAnnotations;

namespace profefolio.Models.DTOs.ClaseMateria
{
    public class ClaseMateriaCreateDTO
    {
        [Required(ErrorMessage = "Requerido el Campo")]
        public List<string> IdProfesores { get; set; }
        [Required(ErrorMessage = "Requerido el Campo")]
        public int IdMateria { get; set; }
        [Required(ErrorMessage = "Requerido el Campo")]
        public int IdClase { get; set; }
    }
}
