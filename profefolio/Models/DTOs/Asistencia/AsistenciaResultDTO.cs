using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.Asistencia
{
    public class AsistenciaResultDTO
    {
        public string Documento { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Nombre { get; set; } = "";

        public List<AssitenciasFechaResult> Asistencias { get; set; } = new();
    }
}