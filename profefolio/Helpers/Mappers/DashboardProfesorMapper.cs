using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using profefolio.Models.DTOs.DashboardProfesor;
using profefolio.Models.DTOs.DashboardProfesor.GetWithOpcions;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers
{
    public class DashboardProfesorMapper : Profile
    {
        public DashboardProfesorMapper()
        {
            CreateMap<ColegioProfesor, ColegiosProfesorDbDTO>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(v => v.Id))
                .ForMember(dest => dest.Nombre,
                    opt => opt.MapFrom(v => v.Colegio != null ? v.Colegio.Nombre : ""))
                .ForMember(dest => dest.Clases,
                    opt => opt.MapFrom(v => new List<string>()))
                .ForMember(dest => dest.Horarios,
                    opt => opt.MapFrom(v => new List<ClasesHorariosProfesorDbDTO>()));




            CreateMap<HorasCatedrasMaterias, ClasesHorariosProfesorDbDTO>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(v => v.MateriaListaId))
                .ForMember(dest => dest.Dia,
                    opt => opt.MapFrom(v => v.Dia))
                .ForMember(dest => dest.Inicio,
                    opt => opt.MapFrom(v => v.HoraCatedra != null ? v.HoraCatedra.Inicio : ""));


            CreateMap<Clase, DBCardClasesDTO>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(v => v.Id))
                .ForMember(dest => dest.Ciclo,
                    opt => opt.MapFrom(v => v.Ciclo.Nombre))
                .ForMember(dest => dest.Anho,
                    opt => opt.MapFrom(v => v.Anho))
                .ForMember(dest => dest.Alumnos, 
                    opt => opt.MapFrom(v => v.ClasesAlumnosColegios.Count()))
                .ForMember(dest => dest.Materias, 
                    opt => opt.MapFrom(v => v.MateriaListas.DistinctBy(a => a.MateriaId).Select(a => a.Materia.Nombre_Materia)))
                .ForMember(dest => dest.Horario, 
                    opt => opt.MapFrom(v => new DBCardClasesHorariosDTO()));
        }
    }
}