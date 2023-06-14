using profefolio.Models.DTOs.Materia;

namespace profefolio.Models.DTOs.ClaseMateria
{
    public class ClaseDetallesDTO
    {
        public int ClaseId { get; set; }
        public string NombreClase { get; set; }
        public List<MateriaProfesoresDTO>? MateriaProfesores { get; set; }


    }
}
