using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;
using profefolio.Models.DTOs.Materia;
using profefolio.Models.DTOs.DashboardProfesor.GetWithOpcions;
using profefolio.Models.DTOs.DashboardPuntajes;

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
        Task<List<ClasesAlumnosColegio>> GetColegioAlumnoId(int idClase, String idProfesor);
        Task<String> FindAlumnoIdByColegioAlumnoId( int idColegiosAlumnos);
        Task<string> GetHorasOfMateriaInDay(Persona profesor, int idMateriaLista, string dia);
        Task<int> GetEventosOfMateria(string idProfesor, int materia, int idClase);

        // horarios de materias en cada clase
        Task<List<HorasCatedrasMaterias>> FindAllHorariosClasesByEmailProfesorAndIdColegio(int idColegio, string email, int anho); 
        Task<DBCardsMateriaInfo> FindDataForCardOfInfoMateria(int idMateriaLista, string emailProfesor);
        Task<MateriaLista> GetPromediosPuntajesByIdMateriaLista(int idMateriaLista, string emailProfesor);
        Task<(double, double, double)> GetPromedioAsistenciasByMonth(int year, int month, int idMateriaLista, string profesorId);
        Task<List<DBCardEventosColegioDTO>> FindEventosOfClase(String idprofesor, int idColegio);
        Task<List<DBCardEventosClaseDTO>> FindEventosOfClase(String idprofesor);
        Task<List<DBCardEventosMateriaDTO>> FindEventosMaterias(String idprofesor, int idClase);
        Task<List<DashboardPuntajeDTO>> ShowPuntajes(string user, int idMateriaLista);
    }
}