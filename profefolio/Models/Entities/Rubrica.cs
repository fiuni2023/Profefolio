namespace profefolio.Models.Entities;

public class Rubrica : Data
{
    public double Puntaje { get; set; }
    public string NombreRubrica { get; set; }
    public Evento Evento { get; set; }
    public int EventoId { get; set; }

}