using AutoMapper;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.ColegioProfesor;
using profefolio.Models.DTOs.Profesor;
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
        
        CreateMap<Persona, ProfesorSimpleDTO>()
            .ForMember(dest => dest.IdProfesor, 
                opt => opt.MapFrom(src => src.Id));


        CreateMap<ColegioProfesorResultOfCreatedDTO, ProfesorGetDTO>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(a => a.IdProfesor))
            .ForMember(dest => dest.Nombre,
                opt => opt.MapFrom(a => a.Nombre))
            .ForMember(dest => dest.Apellido, 
                opt => opt.MapFrom(a => a.Apellido))
            .ForMember(dest => dest.Nacimiento, 
                opt => opt.MapFrom(a => a.Nacimiento))
            .ForMember(dest => dest.Direccion, 
                opt => opt.MapFrom(a => a.Direccion))
            .ForMember(dest => dest.Email, 
                opt => opt.MapFrom(a => a.Email))
            .ForMember(dest => dest.Telefono, 
                opt => opt.MapFrom(a => a.Telefono))
            .ForMember(dest => dest.Genero,
                opt => opt.MapFrom(a => a.Genero))
            .ForMember(dest => dest.Documento, 
                opt => opt.MapFrom(a => a.Documento))
            .ForMember(dest => dest.DocumentoTipo, 
                opt => opt.MapFrom(a => a.DocumentoTipo));
            
            
            
            
            
            
            
        CreateMap<Persona, ProfesorGetDTO>()
            .ForMember(dest => dest.Genero,
                opt => opt.MapFrom(
                    src => src.EsM ? "Masculino" : "Femenino"));
    }
}