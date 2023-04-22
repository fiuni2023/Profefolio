namespace profefolio.Models.DTOs.ClaseMateria
{
    public class ClaseDetallesDTO
    {
        public int ClaseId { get; set; }
        public List<ClaseMateriaDetalle> Detalles { get; set; }
        public int MateriaId { get; set; }


    }
}
