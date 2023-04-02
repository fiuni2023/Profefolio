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
            opt => opt.MapFrom(ca => ca.ColegioId));
            
        }
    }
}