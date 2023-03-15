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

        CreateMap<Colegio, ColegioResultDTO>();

        CreateMap<ColegioResultDTO, Colegio>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.Created, 
                opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Modified, 
                opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, 
                opt => opt.Ignore());

        CreateMap<Colegio, ColegioFullDTO>()
            .ForMember(dest => dest.NombreAdministrador, 
                       opt => opt.MapFrom(src => src.personas.Nombre))
            .ForMember(dest => dest.Apellido, 
                       opt => opt.MapFrom(src => src.personas.Apellido))
            .ForMember(dest => dest.Nacimiento, 
                       opt => opt.MapFrom(src => src.personas.Nacimiento))
            .ForMember(dest => dest.Documento, 
                       opt => opt.MapFrom(src => src.personas.Documento))
            .ForMember(dest => dest.DocumentoTipo, 
                       opt => opt.MapFrom(src => src.personas.DocumentoTipo))
            .ForMember(dest => dest.Direccion, 
                       opt => opt.MapFrom(src => src.personas.Direccion)
            );

        CreateMap<ColegioFullDTO, Colegio>()
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