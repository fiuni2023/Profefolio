using System.ComponentModel.DataAnnotations;

namespace profefolio
{
    public class MateriaListaPutDTO
    {
        [Required(ErrorMessage = "Requerido")]
        public int IdClase { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public List<MateriaMateriaListaDTO> Materias {get; set;}

    }
}