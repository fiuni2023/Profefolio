using profefolio.Models.DTOs.Persona;
using profefolio.Models.DTOs;
using profefolio.Models.Entities;

namespace profefolio.Repository;

public interface PersonaRepository
{
     IEnumerable<Persona> GetAll();
     Persona findById(int id);
     Persona Add(Persona p);
     void Delete(Persona p);
     Persona Edit(int id, Persona p);

}