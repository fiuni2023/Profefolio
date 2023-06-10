using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.Entities
{
    public class ColegiosAlumnos : Data
    {
        public int ColegioId { get; set; }

        public string? PersonaId { get; set; }
        public Colegio Colegio { get; set; }
        public Persona Persona { get; set; } = new();

        public ICollection<ClasesAlumnosColegio> ClasesAlumnosColegios { get; set; } = new List<ClasesAlumnosColegio>();

    }
}