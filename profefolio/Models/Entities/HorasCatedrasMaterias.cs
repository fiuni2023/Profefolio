using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.Entities
{
    public class HorasCatedrasMaterias : Data
    {
        [Required(ErrorMessage = "La hora catedra es necesaria")]
        public int HoraCatedraId { get; set; }

        [Required(ErrorMessage = "La materia es necesaria")]
        public int MateriaListaId { get; set; }

        [Required(ErrorMessage = "El dia es necesario")]
        [RegularExpression(@"^(lunes|martes|miercoles|jueves|viernes|sabado|domingo)$", ErrorMessage = "Lo dias tienen que estar en minusculas")]
        public string? Dia { get; set; }
        public HoraCatedra? HoraCatedra {get; set; }
        public MateriaLista? MateriaLista {get; set; }
    }
}