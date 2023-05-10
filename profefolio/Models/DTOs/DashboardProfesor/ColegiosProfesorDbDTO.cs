using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.DashboardProfesor
{
    public class ColegiosProfesorDbDTO : DataDTO
    {
        public string Nombre { get; set; } = "";
        public List<ClasesHorariosProfesorDbDTO> Clases { get; set; } = new(); 
    }
}