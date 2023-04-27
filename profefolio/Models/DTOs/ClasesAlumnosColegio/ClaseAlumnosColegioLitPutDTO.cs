using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.ClasesAlumnosColegio
{
    public class ClaseAlumnosColegioLitPutDTO
    {
        [Required(ErrorMessage = "El campo de Clase es necesario")]
        public int ClaseId { get; set; }

        [Required(ErrorMessage = "Se necesita de la lista de Alumnos a actualizar")]
        public List<ClasesAlumnosColegioPutDTO> ListaAlumnos { get; set; }
    }
}