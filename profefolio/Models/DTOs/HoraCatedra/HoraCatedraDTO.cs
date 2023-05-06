using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.HoraCatedra
{
    public class HoraCatedraDTO
    {
        [Required(ErrorMessage = "La hora de inicio es requerida")]
        [RegularExpression(@"\b((0|[01][0-9]|2[0-3]):[0-5][0-9]|00:00)\b", ErrorMessage = "El formato debe ser HH:mm con dos digitos en cada cual y estar en el rango e 00:00 y 23:59")]
        public string? Inicio { get; set; }

        [Required(ErrorMessage = "La hora de finalizacion es requerida")]
        [RegularExpression(@"\b((0|[01][0-9]|2[0-3]):[0-5][0-9]|00:00)\b", ErrorMessage = "El formato debe ser HH:mm con dos digitos en cada cual y estar en el rango e 00:00 y 23:59")]
        public string? Fin { get; set; }
    }
}