using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.AnotacionAlumno
{
    public class AnotacionesWithInfoAlumnoResultDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Clase { get; set; }
        public string Ciclo { get; set; }
        public List<string> Materias { get; set; }
        public List<AnotacionAlumnoResultDTO> Anotaciones { get; set; }
    }
}