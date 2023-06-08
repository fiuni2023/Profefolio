using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.Entities
{
    public class AnotacionAlumno : Data
    {
        [Required(ErrorMessage = "Alumno necesario")]
        public int AlumnoId { get; set; }

        [Required(ErrorMessage = "Materia necesaria")]
        public int MateriaListaId {get; set;}
        
        [Required(ErrorMessage = "Titulo necesario")]
        [MinLength(1, ErrorMessage = "El titulo es invalido")]
        public string Titulo { get; set; }
        
        [Required]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Descripcion necesaria")]
        [MinLength(1, ErrorMessage = "La descripcion es invalido")]
        public string Descripcion { get; set; }

        [Required]
        [ForeignKey("AlumnoId")]
        public Persona Alumno { get; set; }

        [Required]
        [ForeignKey("MateriaListaId")]
        public MateriaLista MateriaLista { get; set; }
    }
}