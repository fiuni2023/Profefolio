using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.Clase
{
    public class ClaseResultSimpleDTO : DataDTO
    {
        public int CicloId { get; set; }
        public string? Nombre { get; set; }
        public string? Turno { get; set; }
        public int Anho { get; set; }
        public string? Ciclo { get; set; }
    
    }
}