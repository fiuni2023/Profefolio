using System.ComponentModel.DataAnnotations.Schema;

namespace profefolio.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

[Table("Evaluaciones")]
public class Evaluacion : Data
{
    [Required(ErrorMessage = "Requerido")]
    [MaxLength(32, ErrorMessage = "Parametro no valido")]
    public string? Nombre { get; set; }
    [Required(ErrorMessage = "Requerido")]
    [MaxLength(16, ErrorMessage = "Parametro no valido")]
    public string? Etapa { get; set; }
    public MateriaLista? MateriaList{ get; set; }
    [Required(ErrorMessage = "Requerido")]
    public int MateriaListaId { get; set; }
    [Required(ErrorMessage = "Requerido")]
    [MaxLength(32, ErrorMessage = "Parametro no valido")]
    public string? Tipo { get; set; }
    [Required(ErrorMessage = "Requerido")]
    public DateTime Fecha { get; set; }
    public double PuntajeTotal { get; set; }
    public ICollection<EvaluacionAlumno> EvaluacionAlumnos { get; set; } = new List<EvaluacionAlumno>();
}