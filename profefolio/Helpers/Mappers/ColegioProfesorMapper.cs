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
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(cp => cp.Profesor.Nombre))
            .ForMember(dest => dest.Apellido, opt => opt.MapFrom(cp => cp.Profesor.Apellido))
            .ForMember(dest => dest.Documento, opt => opt.MapFrom(cp => cp.Profesor.Documento))
            .ForMember(dest => dest.TipoDocumento, opt => opt.MapFrom(cp => cp.Profesor.DocumentoTipo));
        }
    }
}