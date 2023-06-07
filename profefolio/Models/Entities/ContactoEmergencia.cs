using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.Entities
{
    public class ContactoEmergencia : Data
    {
        [Required(ErrorMessage = "El alumno es requerido")]
        public int AlumnoId { get; set; }

        [Required(ErrorMessage = "Nombre de contacto requerido")]
        public string Nombre { get; set; } = "";
        
        [Required(ErrorMessage = "Apellido de contacto requerido")]
        public string Apellido { get; set; } = "";

        [Required(ErrorMessage = "Relacion de contacto con el alumno requerido")]
        public string Relacion { get; set; } = "";

        [Required(ErrorMessage = "El numero o correo de contacto es requerido")]
        public string Contacto { get; set; } = "";

        [Required(ErrorMessage = "Alumno requerido")]
        [ForeignKey("AlumnoId")]
        public Persona Alumno { get; set; } = null!;
    }
}