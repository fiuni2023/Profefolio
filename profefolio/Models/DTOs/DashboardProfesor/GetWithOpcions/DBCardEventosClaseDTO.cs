namespace profefolio.Models.DTOs.DashboardProfesor.GetWithOpcions;
using Newtonsoft.Json;

    public class DBCardEventosClaseDTO : DataDTO
    {
       
        public string Tipo { get; set; } = "";
        public DateTime Fecha { get; set; }
        public string nombreMateria { get; set; } = "";
        public string nombreClase { get; set; } = "";
        public string nombreColegio { get; set; } = "";

    }