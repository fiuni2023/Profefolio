using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.DashboardProfesor
{
    public class ClasesHorariosProfesorDbDTO : DataDTO
    {
        public string Dia { get; set; } = "";
        public string Inicio { get; set;} = "";
    }
}