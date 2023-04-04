using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.Colegio
{
    public class ColegioWithAdminDataDTO : DataDTO
    {
        public string? NombreAdministrador { get; set; }
        public string? Apellido { get; set; }
        public string? Nombre { get; set; }
        public string? PersonaId { get; set; }
    }
}