using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using profefolio.Models.DTOs.ColegiosAlumnos;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers
{
    public class ColegiosAlumnosMappper : Profile
    {
        public ColegiosAlumnosMappper()
        {
            CreateMap<ColegiosAlumnos, ColegiosAlumnosDTO>()
            .ForMember(dest => dest.AlumnoId,
                opt => opt.MapFrom(ca => ca.PersonaId))
            .ForMember(dest => dest.ColegioId,
                opt => opt.MapFrom(ca => ca.ColegioId)).ReverseMap();

            CreateMap<ColegiosAlumnos, ColegiosAlumnosResultDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(ca => ca.Id))
            .ForMember(dest => dest.AlumnoId, opt => opt.MapFrom(ca => ca.PersonaId))
            .ForMember(dest => dest.ColegioId, opt => opt.MapFrom(ca => ca.ColegioId))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(ca => ca.Persona.Nombre))
            .ForMember(dest => dest.Apellido, opt => opt.MapFrom(ca => ca.Persona.Apellido))
            .ForMember(dest => dest.Documento, opt => opt.MapFrom(ca => ca.Persona.Documento))
            .ForMember(dest => dest.TipoDocumento, opt => opt.MapFrom(ca => ca.Persona.DocumentoTipo));


            CreateMap<ColegiosAlumnos, ColegioAlumnoListPageDTO>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(v => v.Persona.Id))
                .ForMember(dest => dest.Documento,
                    opt => opt.MapFrom(v => v.Persona.Documento))
                .ForMember(dest => dest.Nombre,
                    opt => opt.MapFrom(v => v.Persona.Nombre))
                .ForMember(dest => dest.Apellido,
                    opt => opt.MapFrom(v => v.Persona.Apellido))
                .ForMember(dest => dest.FechaNacimiento,
                    opt => opt.MapFrom(v => v.Persona.Nacimiento))
                .ForMember(dest => dest.Direccion,
                    opt => opt.MapFrom(v => v.Persona.Direccion))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(v => v.Persona.Email));
        }
    }
}