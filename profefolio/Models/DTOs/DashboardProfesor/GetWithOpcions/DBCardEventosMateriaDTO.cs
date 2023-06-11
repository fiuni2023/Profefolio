namespace profefolio.Models.DTOs.DashboardProfesor.GetWithOpcions;
using Newtonsoft.Json;

    public class DBCardEventosMateriaDTO
    {
       
        public string? Tipo { get; set; } = "";
        public DateTime Fecha { get; set; }
        public string nombreMateria { get; set; } = "";
    }