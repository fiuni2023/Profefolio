using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.Clase
{
    public class ClaseResultDTO : DataDTO
    {
        public int IdColegio { get; set; }
        public int IdCiclo { get; set; }
        public string? Nombre { get; set; }
        public string? Turno { get; set; }
        public int Anho { get; set; }

        public string? Colegio { get; set; }
        public string? Ciclo { get; set; }
    }
}