using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.ColegiosAlumnos
{
    public class ColegiosAlumnosResultDTO : DataDTO
    {
        public int ColegioId { get; set; }
        public string? AlumnoId { get; set; }
        public string? Nombre {get; set;}
        public string? Apellido { get; set; }
        public string? Documento { get; set; }
        public string? TipoDocumento { get; set; }
    }
}