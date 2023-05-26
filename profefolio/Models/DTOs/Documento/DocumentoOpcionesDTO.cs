using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.Documento
{
    public class DocumentoOpcionesDTO
    {
        [Required(ErrorMessage = "Identificador requerido")]
        public int Id { get; set; }
    }
}