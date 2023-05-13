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
    //tipo: examen, parcial, prueba sumatoria, examen
    //fecha
    //materia
    //clase
    //colegio
    [Required(ErrorMessage = "Se tiene que agregar el tipo de evento")]
    public string? Tipo { get; set; }

    [Required(ErrorMessage = "Se tiene que agregar la fecha de evento")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "Se tiene que agregar la materia relacionada al evento")]
    public Materia Materias { get; set; }
    public int MateriaId{ get; set; }

    [Required(ErrorMessage = "Se tiene que agregar la clase relacionada al evento")]
    public Clase Clases { get; set; }
    public int ClaseId { get; set; }

    [Required(ErrorMessage = "Se tiene que agregar el colegio relacionado al evento")]
    public Colegio Colegios { get; set; }
    public int ColegioId { get; set; }

    [JsonIgnore]
    public String? ProfesorId { get; set; }
    public Persona Profesores { get; set; }

}