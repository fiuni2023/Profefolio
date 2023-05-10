using AutoMapper;
using profefolio.Models.DTOs.Materia;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers;

public class MateriaMapper : Profile
{
    public MateriaMapper()
    {
        CreateMap<Materia, MateriaDTO>();

        CreateMap<MateriaDTO, Materia>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.Created, 
                opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Modified, 
                opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, 
                opt => opt.Ignore());
        
        CreateMap<Materia, MateriaResultDTO>();

        CreateMap<MateriaResultDTO, Materia>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.Created, 
                opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Modified, 
                opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, 
                opt => opt.Ignore());
         
       
    }
}