using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.ColegioProfesor
{
    public class ColegioProfesorSimpleDTO : DataDTO
    {
        public int ProfesorId { get; set; }
        public string? Nombre { get; set; }
        public string? Documento { get; set; }
    }
}