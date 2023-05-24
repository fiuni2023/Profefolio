using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IAsistencia : IRepository<Asistencia>
    {
        Task<List<ClasesAlumnosColegio>> FindAll(int idMateriaLista, string userEmail);
        Task<Asistencia> FindByIdAndProfesorId(int idMateriaLista, string profesorId);
        Task<Asistencia> Delete(Asistencia t);
        Task<bool> ExistAsistenciaInDate(int idMateriaLista, int idClaseColegioAlumno, DateTime fecha);

        Task<(double, double, double)> PorcentajeInMonth(int year, int month, int idMateriaLista, string profesorId);
    }
}