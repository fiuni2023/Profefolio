using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.DashboardProfesor.GetWithOpcions
{
    public class DBCardClasesDTO : DataDTO
    {
        public string Ciclo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public int Anho { get; set; } = 0;
        public int Alumnos { get; set; } = 0;
        public List<string> Materias { get; set; } = new List<string>();
        public DBCardClasesHorariosDTO Horario { get; set; } = new();

    }

    
}