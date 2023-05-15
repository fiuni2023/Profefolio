using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;
using profefolio.Models.DTOs.Materia;
namespace profefolio.Repository
{
    public interface IDashboardProfesor : IRepository<ColegioProfesor> 
    {
        Task<(Persona, List<Clase>)> GetClasesForCardClases(int idColegio, string emailProfesor, int anho);
        Task<List<string>> FindMateriasOfClase(Persona profesor, int idClase);
        Task<List<MateriaResultFullDTO>> _FindMateriasOfClase(Persona profesor, int idClase);
        Task<HorasCatedrasMaterias> FindHorarioMasCercano(Persona profesor, int idClase);
        Task<HorasCatedrasMaterias> FindHorarioMasCercanoMateria(Persona profesor, int idMateriaLista);
        Task<string> GetHorasOfClaseInDay(Persona profesor, int idClase, string dia);
        Task<string> GetHorasOfMateriaInDay(Persona profesor, int idMateriaLista, string dia);

        // horarios de materias en cada clase
        Task<List<HorasCatedrasMaterias>> FindAllHorariosClasesByEmailProfesorAndIdColegio(int idColegio, string email, int anho); 
    }
}