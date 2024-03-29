using profefolio.Models.DTOs.ClasesAlumnosColegio;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IClasesAlumnosColegio : IRepository<ClasesAlumnosColegio>
    {
        Task<bool> Exist(int ClaseId, int ColegioA);
        Task<ClasesAlumnosColegio?> FindByClaseIdAndColegioAlumnoId(int ClaseId, int ColegioA);
        Task<List<ClasesAlumnosColegio>?> FindAllByClaseIdAndAdminEmail(int ClaseId, string adminEmail);
        Task<bool> IsAlumnoOfClaseAndMateria(int idAlumno, int idMateria);
        Task AssingFull(int claseId, int caId, string username);

    }
}