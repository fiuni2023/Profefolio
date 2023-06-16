using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.Anotacion
{
    public class AnotacionesGraficoDTO
    {
        public List<string> Clases { get; set; }
        public List<int> Cantidades { get; set; }
    }
}