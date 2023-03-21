using AutoMapper;
using profefolio.Models.DTOs.Alumno;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers;

public class AlumnoMapper : Profile
{
    public AlumnoMapper()
    {
        CreateMap<AlumnoCreateDTO, Persona>()
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
            .ForMember(dest => dest.SecurityStamp,
                opt => opt.MapFrom(
                    t => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.UserName, 
                opt => opt.MapFrom(
                    src => src.Email));


        CreateMap<Persona, AlumnoGetDTO>()
            .ForMember(dest => dest.Genero,
                opt => opt.MapFrom(
                    src => src.EsM ? "Masculino" : "Femenino"));


    }
}