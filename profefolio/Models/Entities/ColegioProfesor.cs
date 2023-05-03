using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.Entities
{
    public class ColegioProfesor : Data
    {
        [Required(ErrorMessage = "Es necesario indicar el Colegio")]
        public int ColegioId { get; set; }

        [Required(ErrorMessage = "Es necesario indicar el Profesor")]
        public string? PersonaId { get; set; }


        public Colegio? Colegio { get; set; }
        public Persona? Persona { get; set; }
    }
}