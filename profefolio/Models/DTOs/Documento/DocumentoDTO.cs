namespace profefolio.Models.DTOs.Documento;
using Newtonsoft.Json;

public class DocumentoDTO
{
    
        public string? Nombre { get; set; }

       
        public string? Enlace { get; set; }

        //profesor que creo el documento
        [JsonIgnore] // Agrega el atributo JsonIgnore al campo ProfesorId
        public String? ProfesorId { get; set; }
        public int MateriaListaId{ get; set; }
    
}