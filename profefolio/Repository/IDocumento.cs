using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface IDocumento : IRepository<Documento>
{
    Task<Documento> FindByNameDocumento(string nombre, int idMateriaL);
    Task<List<Documento>> GetAll(int idMateria, String idPrf);
}