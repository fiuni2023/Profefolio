using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.Entities
{
    public class Asistencia : Data
    {
        [Required(ErrorMessage = "El alumno es necesario")]
        public int ClasesAlumnosColegioId { get; set; }

        [Required(ErrorMessage = "La materia es necesaria")]
        public int MateriaListaId { get; set; }

        [Required(ErrorMessage = "La fecha es necesario")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El estado es necesario")]
        public char Estado { get; set; } = 'A';

        public string Observacion { get; set; } = "";


        public ClasesAlumnosColegio Alumno { get; set; }
        public MateriaLista MateriaLista { get; set; }

    }
}