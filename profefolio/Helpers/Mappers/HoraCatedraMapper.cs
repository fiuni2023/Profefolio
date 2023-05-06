using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using profefolio.Models.DTOs.HoraCatedra;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers
{
    public class HoraCatedraMapper : Profile
    {
        public HoraCatedraMapper()
        {
            CreateMap<HoraCatedra, HoraCatedraResultDTO>()
                .ForMember(dest => dest.Id, 
                    opt => opt.MapFrom(v => v.Id))
                .ForMember(dest => dest.Inicio, 
                    opt => opt.MapFrom(v => v.Inicio))
                .ForMember(dest => dest.Fin, 
                    opt => opt.MapFrom(v => v.Fin));
        }
    }
}