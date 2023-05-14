using AutoMapper;
using profefolio.Models.DTOs.Evento;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers;

public class EventoMapper : Profile
{
    public EventoMapper()
    {
        CreateMap<Evento, EventoDTO>();

        CreateMap<EventoDTO, Evento>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.Created,
                opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Modified,
                opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy,
                opt => opt.Ignore());

        CreateMap<Evento, EventoResultDTO>();

        CreateMap<EventoResultDTO, Evento>()
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