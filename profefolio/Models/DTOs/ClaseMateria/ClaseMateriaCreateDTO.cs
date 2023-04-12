namespace profefolio.Models.DTOs.ClaseMateria
{
    public class ClaseMateriaCreateDTO
    {
        public List<string> IdProfesores { get; set; }
        public int IdMateria { get; set; }
        public int IdClase { get; set; }
    }
}
