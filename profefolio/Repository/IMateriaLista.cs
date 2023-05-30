using profefolio.Models.DTOs.ClaseMateria;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IMateriaLista : IDisposable
    {
        Task<bool> IsUsedMateria(int idMateria);
        Task<ClaseDetallesDTO> FindByIdClase(int idClase, string user);
        Task<List<MateriaLista>> FindByIdClaseAndUser(int idClase, string userEmail, string role);
        Task<bool> Put(string idUser, MateriaListaPutDTO dto);
        Task<Persona> GetProfesorOfMateria(int idMateriaLista, string profesorEmail);
        Task<MateriaLista> FindById(int id);
    }
}
