using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface IDocumento : IRepository<Documento>
{
    Task<Documento> FindByNameDocumento(string nombre);
    Task<List<Documento>> GetAll(int idMateria, String idPrf);
}