using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace profefolio.Models.Entities
{
    public class Ciclo : Data
    {
        [Required(ErrorMessage = "Se tiene que agregar una descripcion")]
        [MaxLength(32)]
        public string? Nombre { get; set; }
    }
}