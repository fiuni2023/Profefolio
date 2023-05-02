using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.ClasesAlumnosColegio
{
    public class ClaseAlumnosColegiosInfoAlumnoDTO : DataDTO
    {
        public int ClaseColegioAlumnoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        
    }
}