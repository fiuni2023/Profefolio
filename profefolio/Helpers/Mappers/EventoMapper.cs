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
        

        CreateMap<Evento, EventoResultFullDTO>()
            .ForMember(dest => dest.NombreMateria,
                        opt => opt.MapFrom(src => src.Materias.Nombre_Materia))
            .ForMember(dest => dest.NombreClase,
                        opt => opt.MapFrom(src => src.Clases.Nombre))
            .ForMember(dest => dest.NombreColegio,
                        opt => opt.MapFrom(src => src.Colegios.Nombre));

        CreateMap<EventoResultFullDTO, Evento>()
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