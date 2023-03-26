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
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ReverseMap();


            CreateMap<Clase, ClaseResultDTO>()
            .ForMember(dest => dest.Ciclo, 
                        opt => opt.MapFrom(v => v.Ciclo.Nombre))
            .ForMember(dest => dest.Colegio, 
                        opt => opt.MapFrom(v => v.Colegio.Nombre))
            .ForMember(dest => dest.IdCiclo,
                        opt => opt.MapFrom(v => v.CicloId))
            .ForMember(dest => dest.IdColegio,
                        opt => opt.MapFrom(v => v.ColegioId));
            

            CreateMap<Clase, ClaseResultSimpleDTO>()
            .ForMember(dest => dest.Ciclo, 
                        opt => opt.MapFrom(v => v.Ciclo.Nombre));
        }
        
    }
}