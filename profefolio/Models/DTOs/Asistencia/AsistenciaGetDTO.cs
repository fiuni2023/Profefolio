using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.Asistencia
{
    public class AsistenciaGetDTO
    {
        [Required(ErrorMessage = "Identificador requerido")]
        public int IdMateriaLista{get; set;} = 0;

        [Required(ErrorMessage = "Mes requerido")]
        public int Mes { get; set; } = 0;
    }
}