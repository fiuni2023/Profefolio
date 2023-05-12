using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.DashboardProfesor
{
    public class GetDashboardOptionsDTO
    {
        [Required(ErrorMessage = "Identificador requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Opcion Requerida")]
        public string Opcion { get; set; } = "";

        public int Anho { get; set; }
    }
}