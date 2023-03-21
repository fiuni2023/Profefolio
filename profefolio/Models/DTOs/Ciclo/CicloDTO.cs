using System.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.Ciclo
{
    public class CicloDTO
    {
        [Required(ErrorMessage = "Se tiene que agregar un nombre.")]
        [MaxLength(32, ErrorMessage = "Se exedio la longitud maxima permitida para el nombre.")]
        public string Nombre { get; set; }
    }
}