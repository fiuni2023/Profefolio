using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;

namespace profefolio.Repository
{
    public interface IClasesAlumnosColegio : IRepository<ClasesAlumnosColegio>
    {
        Task<bool> Exist(int ClaseId, int ColegioA);
        Task<ClasesAlumnosColegio?> FindByClaseIdAndColegioAlumnoId(int ClaseId, int ColegioA);
        Task<List<ClasesAlumnosColegio>?> FindAllByClaseIdAndAdminEmail(int ClaseId, string adminEmail);
    }
}