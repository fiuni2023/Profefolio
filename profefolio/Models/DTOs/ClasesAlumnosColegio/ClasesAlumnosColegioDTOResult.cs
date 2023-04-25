using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.ClasesAlumnosColegio
{
    public class ClasesAlumnosColegioDTOResult : DataDTO
    {
        public int ClaseId { get; set; }
        public int ColegioAlumnoId { get; set; }
        public int ColegioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public string DocumentoTipo { get; set; }
        public string Email { get; set; }
        public string ColegioNombre { get; set; }
        public string ClaseNombre { get; set; }
    }
}