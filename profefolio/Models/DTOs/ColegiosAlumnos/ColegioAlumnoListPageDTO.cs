using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.ColegiosAlumnos
{
    public class ColegioAlumnoListPageDTO
    {
        public string Id { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Nacimiento { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }

    }
}