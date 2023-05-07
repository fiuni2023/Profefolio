using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using profefolio.Models.DTOs.HorasCatedrasMaterias;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers
{
    public class HorasCatedrasMateriasMapper : Profile
    {
        public HorasCatedrasMateriasMapper()
        {
            CreateMap<ColegioProfesor, HorariosColegiosResultDTO>()
                .ForMember(dest => dest.Id, 
                    opt => opt.MapFrom(v => v.ColegioId))
                .ForMember(dest => dest.NombreColegio, 
                    opt => opt.MapFrom(v => v.Colegio.Nombre))
                .ForMember(dest => dest.HorariosMaterias,
                    opt => opt.MapFrom(v => new List<HorarioMateriaDTO>()));

            
            CreateMap<HorasCatedrasMaterias, HorarioMateriaDTO>()
                .ForMember(dest => dest.Id, 
                    opt => opt.MapFrom(v => v.Id))
                .ForMember(dest => dest.Nombre, 
                    opt => opt.MapFrom(v => v.MateriaLista.Materia.Nombre_Materia))
                .ForMember(dest => dest.NombreClase, 
                    opt => opt.MapFrom(v => v.MateriaLista.Clase.Nombre))
                .ForMember(dest => dest.Dia, 
                    opt => opt.MapFrom(v => v.Dia))
                .ForMember(dest => dest.Inicio, 
                    opt => opt.MapFrom(v => v.HoraCatedra.Inicio))
                .ForMember(dest => dest.Fin, 
                    opt => opt.MapFrom(v => v.HoraCatedra.Fin));
        }
    }
}