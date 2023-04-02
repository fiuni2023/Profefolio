using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.ColegioProfesor
{
    public class ColegioProfesorResultDTO : DataDTO
    {
        public int ColegioId { get; set; }
        public string? ProfesorId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Documento { get; set; }
        public string? TipoDocumento { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
    }
}
