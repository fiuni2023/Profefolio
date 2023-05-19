using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace profefolio.Models.Entities
{
    public class Anotacion : Data
    {
        public String Titulo { get; set; }

        public String Cuerpo { get; set; }

        public int MateriaListaId { get; set; } 
        
        public MateriaLista Materia { get; set; }
    }
}