using AutoMapper;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers;

public class PersonaMapper : Profile
{
    public PersonaMapper()
    {
        CreateMap<PersonaDTO, Persona>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.Created,
                opt => opt.MapFrom(
                    src => DateTime.Now))
            .ForMember(dest => dest.Modified,
                opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy,
                opt => opt.Ignore())
            .ForMember(dest => dest.EsM,
                opt => opt.MapFrom(
                    src => src.Genero == null ? false : src.Genero.Equals("M")
                ))
            .ForMember(dest => dest.PhoneNumber,
                opt => opt.MapFrom(
                    src => src.Telefono));

        CreateMap<Persona, PersonaResultDTO>()
            .ForMember(dest => dest.Genero,
                opt => opt.MapFrom(
                    src => src.EsM ? "Masculino" : "Femenino"))
            .ForMember(dest => dest.Telefono,
                opt => opt.MapFrom(
                    src => src.PhoneNumber));

    }
}