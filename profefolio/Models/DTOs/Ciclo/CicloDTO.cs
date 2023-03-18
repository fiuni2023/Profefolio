using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.Ciclo
{
    public class CicloDTO
    {
        [Required(ErrorMessage = "Se tiene que agregar una descripcion")]
        [MaxLength(32)]
        public string Descripcion { get; set; }
    }
}