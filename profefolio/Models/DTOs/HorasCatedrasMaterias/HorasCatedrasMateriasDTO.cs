using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.HorasCatedrasMaterias
{
    public class HorasCatedrasMateriasDTO
    {
        [Required(ErrorMessage = "La hora catedra es requerida")]
        public int HoraCatedraId { get; set; }
        
        [Required(ErrorMessage = "La materia es requerida")]
        public int MateriaListaId { get; set; }
        
        [Required(ErrorMessage = "El dia es necesario")]
        [RegularExpression(@"^(lunes|martes|miercoles|jueves|viernes|sabado|domingo)$", ErrorMessage = "Lo dias tienen que estar en minusculas")]
        public string? Dia { get; set; }
    }
}