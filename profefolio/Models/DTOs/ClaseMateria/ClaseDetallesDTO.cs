namespace profefolio.Models.DTOs.ClaseMateria
{
    public class ClaseDetallesDTO : DataDTO
    {
        public string Clase { get; set; }
        public List<string> Profes { get; set; }
        public string Materia { get; set; }


    }
}
