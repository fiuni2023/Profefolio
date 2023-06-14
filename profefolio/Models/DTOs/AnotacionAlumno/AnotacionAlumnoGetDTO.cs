using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.AnotacionAlumno
{
    public class AnotacionAlumnoGetDTO
    {
        [Required(ErrorMessage = "El alumno de la clase es requerido")]
        public int AlumnoId { get; set; }
        
        [Required(ErrorMessage = "La clase es requerida")]
        public int ClaseId { get; set; }
    }
}