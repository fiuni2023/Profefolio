using System.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Models.DTOs.Anotacion
{
    public class AnotacionCreateDTO
    {
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int MateriaListaId { get; set; }
    }
}