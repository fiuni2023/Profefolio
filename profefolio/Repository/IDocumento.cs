using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface IDocumento : IRepository<Documento>
{
    Task<Documento> FindByNameDocumento(string nombre, int idMateriaL);
    Task<bool> FindProfesorIdByDocumento(int idMateriaLista, string idPrf);
    Task<List<Documento>> GetAll(int idMateria, String idPrf);
}