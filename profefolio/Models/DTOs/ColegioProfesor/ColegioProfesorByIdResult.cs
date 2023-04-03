using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.ColegioProfesor
{
    public class ColegioProfesorByIdResult : DataDTO
    {
        public string? ProfesorId{ get; set; }
        public string? Nombre{ get; set; }
        public string? Apellido{ get; set; }

        public DateTime Nacimiento{ get; set; }

        public string? Documento{ get; set; }

        public string? DocumentoTipo{ get; set; }

        public string? Direccion{ get; set; }

        public string? Telefono{ get; set; }

        public string? Email { get; set; }

        public int ColegioId { get; set; }
        public string? NombreColegio { get; set; }
    }
}