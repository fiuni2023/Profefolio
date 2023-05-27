namespace profefolio.Models.Entities;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Models.DTOs.Materia;
using Models.DTOs.Clase;
using Models.DTOs.Colegio;
using Models.DTOs.Persona;

public class Evento : Data
{
    public MateriaLista? MateriaList{ get; set; }
    public int MateriaListaId { get; set; }
    public string? Tipo { get; set; }
    public DateTime Fecha { get; set; }
    public double PuntajeTotal { get; set; }
}