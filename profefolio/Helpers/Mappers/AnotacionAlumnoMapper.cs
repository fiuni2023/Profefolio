using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using profefolio.Models.DTOs.AnotacionAlumno;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers
{
    public class AnotacionAlumnoMapper : Profile
    {
        public AnotacionAlumnoMapper()
        {
            CreateMap<AnotacionAlumno, AnotacionAlumnoResultDTO>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(v => v.Id))
                .ForMember(dest => dest.Titulo,
                    opt => opt.MapFrom(v => v.Titulo))
                .ForMember(dest => dest.Descripcion,
                    opt => opt.MapFrom(v => v.Descripcion))
                .ForMember(dest => dest.Fecha,
                    opt => opt.MapFrom(v => v.Fecha));
        

            CreateMap<AnotacionAlumnoCreateDTO, AnotacionAlumno>()
                .ForMember(dest => dest.AlumnoId,
                    opt => opt.MapFrom(v => v.AlumnoId))
                .ForMember(dest => dest.MateriaListaId,
                    opt => opt.MapFrom(v => v.MateriaListaId))
                .ForMember(dest => dest.Fecha,
                    opt => opt.MapFrom(v => v.Fecha))
                .ForMember(dest => dest.Titulo,
                    opt => opt.MapFrom(v => v.Titulo))
                .ForMember(dest => dest.Descripcion,
                    opt => opt.MapFrom(v => v.Descripcion));
        }
    }
}