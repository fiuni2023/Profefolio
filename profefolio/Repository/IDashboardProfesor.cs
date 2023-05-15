using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IDashboardProfesor : IRepository<ColegioProfesor> 
    {
        Task<(Persona, List<Clase>)> GetClasesForCardClases(int idColegio, string emailProfesor, int anho);
        Task<List<string>> FindMateriasOfClase(Persona profesor, int idClase);
        Task<HorasCatedrasMaterias> FindHorarioMasCercano(Persona profesor, int idClase);
        Task<string> GetHorasOfClaseInDay(Persona profesor, int idClase, string dia);
        Task<List<ClasesAlumnosColegio>> GetColegioAlumnoId(int idClase, String idProfesor);
        Task<String> FindAlumnoIdByColegioAlumnoId( int idColegiosAlumnos);
        // horarios de materias en cada clase
        Task<List<HorasCatedrasMaterias>> FindAllHorariosClasesByEmailProfesorAndIdColegio(int idColegio, string email, int anho); 
    }
}