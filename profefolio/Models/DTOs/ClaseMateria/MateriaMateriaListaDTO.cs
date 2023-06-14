using System.ComponentModel.DataAnnotations;

namespace profefolio
{
    public class MateriaMateriaListaDTO
    {
        [Required(ErrorMessage = "Requerido")]
        public int IdMateria {get; set;}
        [Required(ErrorMessage = "Requerido")]
        public List<ProfesoresEstadosDTO> Profesores {get; set;} 
    }
    
}