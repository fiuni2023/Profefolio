using AutoMapper;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers;

public class PersonaMapper : Profile
{
    public PersonaMapper()
    {
        CreateMap<Persona, PersonaDTO>();

        CreateMap<PersonaDTO, Persona>()
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