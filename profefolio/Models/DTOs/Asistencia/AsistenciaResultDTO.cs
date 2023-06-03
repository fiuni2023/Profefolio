using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.Asistencia
{
    public class AsistenciaResultDTO : DataDTO
    {
        public string Documento { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Nombre { get; set; } = "";
        public double PorcentajePresentes { get; set; } = 0.0;
        public double PorcentajeAusentes { get; set; } = 0.0;
        public double PorcentajeJustificados { get; set; } = 0.0;
        public List<AssitenciasFechaResult> Asistencias { get; set; } = new();
    }
}