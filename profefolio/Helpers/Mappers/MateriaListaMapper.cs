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
                    opt => opt.MapFrom(v => new List<ClaseMateriaProfesorDTO>()));

            CreateMap<MateriaLista, ClaseDetallesDTO>()
                .ForMember(dest => dest.MateriaId,
                    opt => opt.MapFrom(src => src.MateriaId))
                .ForMember(dest => dest.ClaseId,
                    opt => opt.MapFrom(src => src.ClaseId))
                .ForMember(dest => dest.NombreClase,
                    opt => opt.MapFrom(src => src.Clase == null ? "" : src.Clase.Nombre))
                .ForMember(dest => dest.Materia,
                    opt => opt.MapFrom(src => src.Materia == null ? "" : src.Materia.Nombre_Materia))
                .ForMember(dest => dest.Profesores, opt => opt.Ignore());


        }
    }
}