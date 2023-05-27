using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.DashboardProfesor.GetWithOpcions
{
    public class DBPromedioAsistenciasDTO
    {
        public string Mes { get; set; } = "";
        public double Presentes { get; set; } = 0.0;
        public double Ausentes { get; set; } = 0.0;
        public double Justificados { get; set; } = 0.0;
        
    }
}