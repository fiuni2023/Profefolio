using profefolio.Models.DTOs.Materia;

namespace profefolio.Models.DTOs.ClaseMateria
{
    public class ClaseDetallesDTO
    {
        public int ClaseId { get; set; }
        public string NombreClase { get; set; }
        public List<ProfesorSimpleDTO> Profesores { get; set; }
        public List<MateriaDTO> Materias {get; set; }

    }
}
