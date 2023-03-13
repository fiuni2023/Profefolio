using AutoMapper;
using profefolio.Models.DTOs;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers;

public class ProfesorMapper : Profile
{
    public ProfesorMapper()
    {
        CreateMap<ProfesorDTO, Persona>()
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
                    src => src.Genero != null && src.Genero.Equals("M")
                ))
            .ForMember(dest => dest.PhoneNumber,
                opt => opt.MapFrom(
                    src => src.Telefono))
            .ForMember(dest => dest.SecurityStamp,
                opt => opt.MapFrom(
                    t => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.UserName, 
                opt => opt.MapFrom(
                    dest => dest.Email))
            .ForMember(dest => dest.NormalizedEmail,
                opt => opt.MapFrom(
                    src =>src.Email == null? "" : src.Email.ToUpper()))
            .ForMember(dest => dest.NormalizedUserName,
                opt => opt.MapFrom(
                    src => src.Email == null ? "" : src.Email.ToUpper()));
        

    }
}