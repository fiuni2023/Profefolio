using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.DashboardProfesor.GetWithOpcions
{
    public class DBAlumnosClaseDTO : DataDTO
    {
       
        public int PersonaId { get; set; } = 0; //id del alumno en la tabla de ColegioAlumnos
        public string Nombres { get; set; } = "";
        public string Apellidos { get; set; } = "";

    }

    
}