using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace profefolio.Models.Entities
{
    public class Documento : Data
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
        public int MateriaListaId{ get; set; }
    }
}