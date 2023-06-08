using AutoMapper;
using profefolio.Models.DTOs.Evento;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers;

public class EventoMapper : Profile
{
    public EventoMapper()
    {
        CreateMap<Evaluacion, EventoDTO>();

        CreateMap<EventoDTO, Evaluacion>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.Created,
                opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Modified,
                opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy,
                opt => opt.Ignore());

        CreateMap<Evaluacion, EventoResultDTO>();

        CreateMap<EventoResultDTO, Evaluacion>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.Created,
                opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Modified,
                opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy,
                opt => opt.Ignore());
        
        
        CreateMap<Evaluacion, EventoResultFullDTO>()
            .ForMember(dest => dest.NombreMateria,
                        opt => opt.Ignore())
            .ForMember(dest => dest.NombreClase,
                        opt => opt.Ignore())
            .ForMember(dest => dest.NombreColegio,
                        opt => opt.Ignore());
        
        CreateMap<EventoResultFullDTO, Evaluacion>()
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