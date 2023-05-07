using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.HorasCatedrasMaterias
{
    public class HorariosColegiosResultDTO : DataDTO
    {
        public string? NombreColegio { get; set; }
        public List<HorarioMateria>? HorariosMaterias { get; set; }
    }
}