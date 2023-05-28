using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.Entities
{
    public class ClasesAlumnosColegio : Data
    {
        [Required(ErrorMessage = "El campo de Id de Clase es Requerido")]
        public int ClaseId { get; set; }

        [Required(ErrorMessage = "El campo de Id de Alumno en el colegio es requerido")]
        public int ColegiosAlumnosId { get; set; }

        public Clase? Clase { get; set; }
        public ColegiosAlumnos? ColegiosAlumnos { get; set; }

        public IEnumerable<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
    }
}