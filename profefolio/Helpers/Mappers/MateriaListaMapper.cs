using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using profefolio.Models.DTOs.ClaseMateria;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers
{
    public class MateriaListaMapper : Profile
    {

        public MateriaListaMapper()
        {
            CreateMap<Persona, ClaseMateriaProfesorDTO>()
                .ForMember(dest => dest.Id,  
                    opt => opt.MapFrom(v => v.Id))
                .ForMember(dest => dest.Nombre,  
                    opt => opt.MapFrom(v => v.Nombre))
                .ForMember(dest => dest.Apellido,  
                    opt => opt.MapFrom(v => v.Apellido));


            CreateMap<MateriaLista, ClaseMateriaResultDTO>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(v => v.Id))
                .ForMember(dest => dest.Nombre,
                    opt => opt.MapFrom(v => v.Materia.Nombre_Materia))
                .ForMember(dest => dest.Profesores,
                    opt => opt.MapFrom(v => new ClaseMateriaProfesorDTO() {
                        Id = v.Profesor.Id,
                        Nombre = v.Profesor.Nombre,
                        Apellido = v.Profesor.Apellido
                    }));
        }
    }
}