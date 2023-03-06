using AutoMapper;
using profefolio.Models.DTOs.Colegio;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers;

public class ColegioMapper : Profile
{
    public ColegioMapper()
    {
        CreateMap<Colegio, ColegioDTO>();

        CreateMap<ColegioDTO, Colegio>()
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