using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using profefolio.Models.DTOs.ClasesAlumnosColegio;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers
{
    public class ClasesAlumnosColegioMapper : Profile
    {
        public ClasesAlumnosColegioMapper()
        {
            CreateMap<ClasesAlumnosColegioDTO, ClasesAlumnosColegio>()
            .ForMember(dest => dest.ClaseId,
                        opt => opt.MapFrom(v => v.ClaseId))
            .ForMember(dest => dest.ColegiosAlumnosId,
                        opt => opt.MapFrom(v => v.ColegioAlumnoId)).ReverseMap();
        }
    }
}