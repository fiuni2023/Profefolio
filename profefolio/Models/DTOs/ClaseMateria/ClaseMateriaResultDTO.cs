using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.ClaseMateria
{
    public class ClaseMateriaResultDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ClaseMateriaProfesorDTO> Profesores { get; set; } = new();
    }
}