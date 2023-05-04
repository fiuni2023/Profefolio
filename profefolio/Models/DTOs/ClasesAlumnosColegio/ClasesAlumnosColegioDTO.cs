using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.ClasesAlumnosColegio
{
    public class ClasesAlumnosColegioDTO
    {
        [Required(ErrorMessage = "El campo de Id de Clase es necesario")]
        public int ClaseId { get; set; }

        [Required(ErrorMessage = "El campo de Id del Alumno en el Colegio es necesario")]
        public int ColegioAlumnoId { get; set; }
    }
}