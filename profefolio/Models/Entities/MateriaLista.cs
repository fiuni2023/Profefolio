using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace profefolio.Models.Entities
{
    public class MateriaLista : Data
    {
        public string ProfesorId { get; set; }
        public int ClaseId { get; set; }
        public int MateriaId { get; set; }
        [Required]
        [ForeignKey("MateriaId")]
        public Materia Materia { get; set; }
        [Required]
        [ForeignKey("ProfesorId")]
        public Persona Profesor { get; set; }
        public Clase Clase { get; set; }
        public ICollection<Evento>? Eventos { get; set; }
        public IEnumerable<HorasCatedrasMaterias>? Horarios { get; set; }

        public IEnumerable<Asistencia>? ListaAsistencias { get; set; }
    }
}
