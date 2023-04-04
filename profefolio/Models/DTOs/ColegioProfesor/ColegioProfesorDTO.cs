using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;

namespace profefolio.Models.DTOs.ColegioProfesor
{
    public class ColegioProfesorDTO
    {
        [Required(ErrorMessage = "Es necesario el Colegio")]
        public int ColegioId { get; set; }
        
        [Required(ErrorMessage = "Es necesario el Profesor")]
        public string? ProfesorId { get; set; }

    }
}