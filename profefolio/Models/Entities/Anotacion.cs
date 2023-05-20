using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace profefolio.Models.Entities
{
    public class Anotacion : Data
    {
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int MateriaListaId { get; set; }
        public MateriaLista MateriaLista { get; set; }
    }
}