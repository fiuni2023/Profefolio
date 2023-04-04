using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.Entities
{
    public class Clase : Data
    {

        [Required(ErrorMessage ="Se tiene que indicar el Colegio")]
        public int ColegioId { get; set; }

        [Required(ErrorMessage ="Se tiene que indicar el Ciclo")]
        public int CicloId { get; set; }

        [Required(ErrorMessage = "Se tiene que agregar un nombre a la clase")]
        [MaxLength(128)]
        public string? Nombre { get; set; }
        
        [Required(ErrorMessage = "Se tiene que agregar el turno")]
        [MaxLength(32)]
        public string? Turno { get; set; }

        [Required(ErrorMessage = "Se tiene que agregar el anho")]
        public int Anho { get; set; }

        
        public Ciclo? Ciclo { get; set; }
        public Colegio? Colegio { get; set; }

    }
}