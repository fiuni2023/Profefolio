using System.ComponentModel.DataAnnotations;

namespace profefolio
{
    public class ProfesoresEstadosDTO
    {
        [Required(ErrorMessage = "Requerido")]
        public string IdProfesor { get; set; }
        public char Estado { get; set; }
    }
    
}