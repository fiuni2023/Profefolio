namespace profefolio
{
    public class ColegioAlumnosDTO
    {
        public int IdColegioAlumno { get; set; }
        public string Id{ get; set; }
        public int IdColegio { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Nacimiento { get; set; }
        public string Documento { get; set; }
        public string DocumentoTipo { get; set; }
        public string Genero { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
    }

}