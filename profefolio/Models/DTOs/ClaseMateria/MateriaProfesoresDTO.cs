namespace profefolio
{
    public class MateriaProfesoresDTO
    {
        public int IdMateria { get; set; }
        public string Materia { get; set; }
        public List<ProfesorSimpleDTO> Profesores { get; set; }
    }
}