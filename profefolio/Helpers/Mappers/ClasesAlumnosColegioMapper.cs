using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using profefolio.Models.DTOs.ClasesAlumnosColegio;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers
{
    public class ClasesAlumnosColegioMapper : Profile
    {
        public ClasesAlumnosColegioMapper()
        {
            CreateMap<ClasesAlumnosColegioDTO, ClasesAlumnosColegio>()
            .ForMember(dest => dest.ClaseId,
                        opt => opt.MapFrom(v => v.ClaseId))
            .ForMember(dest => dest.ColegiosAlumnosId,
                        opt => opt.MapFrom(v => v.ColegioAlumnoId)).ReverseMap();
        

            CreateMap<ClasesAlumnosColegio, ClasesAlumnosColegioDTOResult>()
            .ForMember(dest => dest.Id, 
                        opt => opt.MapFrom(v => v.Id))
            .ForMember(dest => dest.ClaseId, 
                        opt => opt.MapFrom(v => v.ClaseId))
            .ForMember(dest => dest.ColegioAlumnoId, 
                        opt => opt.MapFrom(v => v.ColegiosAlumnosId))
            .ForMember(dest => dest.ColegioId, 
                        opt => opt.MapFrom(v => v.Clase.ColegioId))
            .ForMember(dest => dest.Nombre, 
                        opt => opt.MapFrom(v => v.ColegiosAlumnos.Persona.Nombre))
            .ForMember(dest => dest.Apellido, 
                        opt => opt.MapFrom(v => v.ColegiosAlumnos.Persona.Apellido))
            .ForMember(dest => dest.Documento, 
                        opt => opt.MapFrom(v => v.ColegiosAlumnos.Persona.Documento))
            .ForMember(dest => dest.DocumentoTipo, 
                        opt => opt.MapFrom(v => v.ColegiosAlumnos.Persona.DocumentoTipo))
            .ForMember(dest => dest.Email, 
                        opt => opt.MapFrom(v => v.ColegiosAlumnos.Persona.Email))
            .ForMember(dest => dest.ColegioNombre, 
                        opt => opt.MapFrom(v => v.ColegiosAlumnos.Colegio.Nombre))
            .ForMember(dest => dest.ClaseNombre, 
                        opt => opt.MapFrom(v => v.Clase.Nombre));

            
            CreateMap<ClasesAlumnosColegio, ClaseAlumnosColegiosInfoAlumnoDTO>()
                .ForMember(dest => dest.Id, 
                    opt => opt.MapFrom(v => v.ColegiosAlumnosId))
                .ForMember(dest => dest.ClaseColegioAlumnoId, 
                    opt => opt.MapFrom(v => v.Id))
                .ForMember(dest => dest.Nombre, 
                    opt => opt.MapFrom(v => v.ColegiosAlumnos.Persona.Nombre))
                .ForMember(dest => dest.Apellido, 
                    opt => opt.MapFrom(v => v.ColegiosAlumnos.Persona.Apellido))
                .ForMember(dest => dest.Documento, 
                    opt => opt.MapFrom(v => v.ColegiosAlumnos.Persona.Documento));

            CreateMap<ColegiosAlumnos, ColegioAlumnosDTO>()
            .ForMember(dest => dest.Genero,
                opt => opt.MapFrom(
                    src => src.Persona == null ? "" : src.Persona.EsM ? "M" : "F"));
        }
    }
}