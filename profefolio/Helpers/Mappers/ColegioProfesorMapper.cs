using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using profefolio.Models.DTOs.ColegioProfesor;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers
{
    public class ColegioProfesorMapper : Profile
    {
        public ColegioProfesorMapper()
        {
            CreateMap<ColegioProfesor, ColegioProfesorDTO>()
            .ForMember(dest => dest.ProfesorId,
                opt => opt.MapFrom(cp => cp.PersonaId))
            .ForMember(dest => dest.ColegioId,
                opt => opt.MapFrom(cp => cp.ColegioId)).ReverseMap();

            CreateMap<ColegioProfesor, ColegioProfesorResultDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(cp => cp.Id))
            .ForMember(dest => dest.ProfesorId, opt => opt.MapFrom(cp => cp.PersonaId))
            .ForMember(dest => dest.ColegioId, opt => opt.MapFrom(cp => cp.ColegioId))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(cp => cp.Persona.Nombre))
            .ForMember(dest => dest.Apellido, opt => opt.MapFrom(cp => cp.Persona.Apellido))
            .ForMember(dest => dest.Documento, opt => opt.MapFrom(cp => cp.Persona.Documento))
            .ForMember(dest => dest.TipoDocumento, opt => opt.MapFrom(cp => cp.Persona.DocumentoTipo))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(cp => cp.Persona.PhoneNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(cp => cp.Persona.Email));


            CreateMap<ColegioProfesor, ColegioProfesorByIdResult>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(v => v.Id))
                .ForMember(dest => dest.ColegioId,
                    opt => opt.MapFrom(v => v.ColegioId))
                .ForMember(dest => dest.ProfesorId,
                    opt => opt.MapFrom(v => v.PersonaId))
                .ForMember(dest => dest.Nombre,
                    opt => opt.MapFrom(v => v.Persona.Nombre))
                .ForMember(dest => dest.Apellido,
                    opt => opt.MapFrom(v => v.Persona.Apellido))
                .ForMember(dest => dest.Direccion,
                    opt => opt.MapFrom(v => v.Persona.Direccion))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(v => v.Persona.Email))
                .ForMember(dest => dest.Documento,
                    opt => opt.MapFrom(v => v.Persona.Documento))
                .ForMember(dest => dest.DocumentoTipo,
                    opt => opt.MapFrom(v => v.Persona.DocumentoTipo))
                .ForMember(dest => dest.Nacimiento,
                    opt => opt.MapFrom(v => v.Persona.Nacimiento))
                .ForMember(dest => dest.NombreColegio,
                    opt => opt.MapFrom(v => v.Colegio.Nombre))
                .ForMember(dest => dest.Telefono,
                    opt => opt.MapFrom(v => v.Persona.PhoneNumber))
                .ForMember(dest => dest.Genero,
                    opt => opt.MapFrom(src => src.Persona.EsM ? "Masculino" : "Femenino"));


            CreateMap<ColegioProfesor, ColegioProfesorSimpleDTO>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(v => v.Id))
                .ForMember(dest => dest.Nombre,
                    opt => opt.MapFrom(v => $"{v.Persona.Nombre} {v.Persona.Apellido}"))
                .ForMember(dest => dest.Documento,
                    opt => opt.MapFrom(v => v.Persona.Documento))
                .ForMember(dest => dest.ProfesorId,
                    opt => opt.MapFrom(v => v.PersonaId));
        }
    }
}