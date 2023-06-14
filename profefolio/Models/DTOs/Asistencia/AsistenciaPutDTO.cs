using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.Asistencia
{
    public class AsistenciaPutDTO : DataDTO
    {
        public DateTime Fecha { get; set; }
        
        [Required(ErrorMessage = "El campo de Estado es necesario")]
        [RegularExpression(@"^[PAJ]$", ErrorMessage = "El estado tiene que ser P(Presente) o A(Ausente) o J(Justificado)")]
        public char Estado { get; set; }
        
        [Required(ErrorMessage = "El campo de Accion es necesario")]
        [RegularExpression(@"^[NUD]$", ErrorMessage = "El estado tiene que ser N(new) o U(updated) o D(deleted)")]
        public char Accion { get; set; }
        public string Observacion { get; set; } = "";
    }
}