namespace profefolio.Models.DTOs.DashboardProfesor.GetWithOpcions
{
    public class DBCardMateriasDTO : DataDTO
    {
       
        public string Nombre { get; set; } = "";
        public int Anotaciones { get; set; } = 0;
        public int Calificaciones { get; set; } = 0;
        public int Eventos { get; set; } = 0;
        public DBCardClasesHorariosDTO Horario { get; set; } = new();

    }

    
}