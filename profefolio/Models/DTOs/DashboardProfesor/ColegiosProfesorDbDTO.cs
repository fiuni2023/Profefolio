using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.DashboardProfesor
{
    public class ColegiosProfesorDbDTO : DataDTO
    {
        public string Nombre { get; set; } = "";
        public List<string> Clases { get; set; } = new(); 
        public List<ClasesHorariosProfesorDbDTO> Horarios { get; set; } = new(); 
    }
}