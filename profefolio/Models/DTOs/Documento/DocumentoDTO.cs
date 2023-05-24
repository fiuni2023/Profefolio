namespace profefolio.Models.DTOs.Evento;
using Newtonsoft.Json;

public class DocumentoDTO
{
     [Required(ErrorMessage = "Se tiene que agregar un nombre")]
        [MaxLength(32)]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Se tiene que agregar un enlace")]
        [MaxLength(100)]
        public string? Enlace { get; set; }

        //profesor que creo el documento
        [JsonIgnore] // Agrega el atributo JsonIgnore al campo ProfesorId
        public String? ProfesorId { get; set; }
    
}