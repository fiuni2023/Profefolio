using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace profefolio.Models.Entities
{
    public class MateriaLista : Data
    {
        public int ProfesorId { get; set; }
        public int ClaseId { get; set; }
        public int MateriaId { get; set; }
        [Required]
        [ForeignKey("MateriaId")]
        Materia Materia { get; set; }

        [Required]
        [ForeignKey("ProfesorId")]
        Persona Profesor { get; set; }
        public Clase Clase { get; set; }
    }
}
