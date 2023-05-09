using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.HoraCatedra
{
    public class HoraCatedraResultDTO : DataDTO
    {
        public string? Inicio { get; set; }
        public string? Fin { get; set; }
    }
}