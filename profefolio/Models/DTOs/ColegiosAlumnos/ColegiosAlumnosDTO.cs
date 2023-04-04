using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.ColegiosAlumnos
{
    public class ColegiosAlumnosDTO
    {
        [Required(ErrorMessage = "El Colegio es requerido")]
        public int ColegioId { get; set; }
        
        [Required(ErrorMessage = "El Alumno es requerido")]
        public string? AlumnoId { get; set; }
    }
}