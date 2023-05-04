using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.ClasesAlumnosColegio
{
    public class ClasesAlumnosColegioPutDTO
    {
        [Required(ErrorMessage = "El campo de Alumno de Colegio es necesario")]
        public int ColegioAlumnoId { get; set; }

        [Required(ErrorMessage = "El campo de Clase es necesario")]
        [RegularExpression(@"^[ND]$", ErrorMessage = "El estado tiene que ser N(new) o D(deleted)")]
        public char Estado { get; set; }
    }
}