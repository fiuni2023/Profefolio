using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using profefolio.Models.DTOs.DashboardProfesor;
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
                    opt => opt.MapFrom(v => new List<ClasesHorariosProfesorDbDTO>()));




            CreateMap<HorasCatedrasMaterias, ClasesHorariosProfesorDbDTO>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(v => v.MateriaListaId))
                .ForMember(dest => dest.Nombre,
                    opt => opt.MapFrom(v => v.MateriaLista != null ? v.MateriaLista.Clase.Nombre : ""))
                .ForMember(dest => dest.Dia,
                    opt => opt.MapFrom(v => v.Dia))
                .ForMember(dest => dest.Inicio,
                    opt => opt.MapFrom(v => v.HoraCatedra != null ? v.HoraCatedra.Inicio : ""))
                .ForMember(dest => dest.Fin,
                    opt => opt.MapFrom(v => v.HoraCatedra != null ? v.HoraCatedra.Fin : ""));

        }



    }
}