using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.DashboardProfesor.GetWithOpcions
{
    public class DBHorariosClasesCalendarDTO : DataDTO
    {
        public string Nombre { get; set; } = "";
        public string Dia { get; set; } = "";
        public string Inicio { get; set; } = "";
        public string Fin { get; set; } = "";
        
    }
}