using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.ContactoEmergencia
{
    public class ContactoEmergenciaCreateDTO
    {
        [Required(ErrorMessage = "Alumno requerido")]
        public string AlumnoId { get; set; }
        [Required(ErrorMessage = "Nombre de contacto requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Apellido de contacto requerido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "La relacion entre el alumno y el contacto es requerido")]
        public string Relacion { get; set; }
        [Required(ErrorMessage = "El numero o correo de contacto es requerido")]
        public string Contacto { get; set; }
    }
}