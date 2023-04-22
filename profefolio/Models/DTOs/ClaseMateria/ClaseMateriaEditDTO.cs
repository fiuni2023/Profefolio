namespace profefolio
{
    public class ClaseMateriaEditDTO
    {
        public int Id {get; set;}
        public int IdClase {get; set;}
        public int IdMateria {get; set;}
        public List<ClaseMateriaDetalle> IdListas {get; set;}
    }
}