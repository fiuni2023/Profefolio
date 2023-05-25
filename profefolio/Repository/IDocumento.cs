using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface IDocumento : IRepository<Documento>
{
    Task<Documento> FindByNameDocumento(string nombre);
}