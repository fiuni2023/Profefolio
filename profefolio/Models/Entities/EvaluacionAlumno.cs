using System.ComponentModel.DataAnnotations.Schema;

namespace profefolio.Models.Entities;
using System.ComponentModel.DataAnnotations;
[Table("EvaluacionAlumnos")]
public class EvaluacionAlumno : Data
{
    public Evaluacion? Evaluacion { get; set; }
    [Required(ErrorMessage = "Requerido")]
    public int EvaluacionId { get; set; }
    public double PuntajeLogrado { get; set; }
    public double PorcentajeLogrado { get; set; }
    public ClasesAlumnosColegio? ClasesAlumnosColegio { get; set; }
    [Required(ErrorMessage = "Requerido")]
    public int ClasesAlumnosColegioId { get; set; }

}