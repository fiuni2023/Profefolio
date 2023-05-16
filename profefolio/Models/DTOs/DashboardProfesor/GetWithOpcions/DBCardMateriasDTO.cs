namespace profefolio.Models.DTOs.DashboardProfesor.GetWithOpcions;
using Newtonsoft.Json;

    public class DBCardMateriasDTO : DataDTO
    {
       
        public string Nombre { get; set; } = "";
        public int Anotaciones { get; set; } = 0;

        [JsonIgnore]public int MateriaId { get; set; } = 0;
        public int Calificaciones { get; set; } = 0;
        public int Eventos { get; set; } = 0;
        public DBCardClasesHorariosDTO Horario { get; set; } = new();

    }