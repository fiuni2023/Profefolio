using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.DashboardProfesor.GetWithOpcions
{
    public class DBCardsMateriaInfo
    {
        public int Anotaciones { get; set; }
        public DBCardsMateriaInfoCalificaciones Calificaciones { get; set; } = new();
        public int Asistencias { get; set; }
        public int Documentos { get; set; } 
    }

}