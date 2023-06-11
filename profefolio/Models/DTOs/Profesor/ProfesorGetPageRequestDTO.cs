using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.Profesor
{
    public class ProfesorGetPageRequestDTO
    {
        public int Pagina { get; set; } = 0;
        [Required(ErrorMessage = "El Colegio es necesario")]
        public int ColegioId { get; set; }
    }
}