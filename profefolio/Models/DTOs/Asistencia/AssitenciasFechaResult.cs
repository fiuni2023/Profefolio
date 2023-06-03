using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.Asistencia
{
    public class AssitenciasFechaResult : DataDTO
    {
        public DateTime Fecha { get; set; }
        public char Estado { get; set; }
        public string Observacion { get; set; } = "";
    }
}