using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using profefolio.Models.DTOs.Clase;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers
{
    public class ClaseMapper : Profile
    {
        public ClaseMapper()
        {   
            CreateMap<ClaseDTO, Clase>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Deleted, opt => opt.MapFrom(v => false))
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Modified, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore()).ReverseMap();


            CreateMap<Clase, ClaseResultDTO>();
        }
        
    }
}