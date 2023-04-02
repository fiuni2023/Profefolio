using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;

namespace profefolio.Models.DTOs.ColegioProfesor
{
    public class ColegioProfesorDTO : Data
    {
        public int ColegioId { get; set; }
        public string? ProfesorId { get; set; }

    }
}