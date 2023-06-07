using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using profefolio.Models.DTOs.ContactoEmergencia;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers
{
    public class ContactoEmergenciaMapper : Profile
    {
        public ContactoEmergenciaMapper()
        {
            CreateMap<ContactoEmergenciaCreateDTO, ContactoEmergencia>()
                .ForMember(dest => dest.AlumnoId, 
                    opt => opt.MapFrom(v => v.AlumnoId))
                .ForMember(dest => dest.Nombre, 
                    opt => opt.MapFrom(v => v.Nombre))
                .ForMember(dest => dest.Apellido, 
                    opt => opt.MapFrom(v => v.Apellido))
                .ForMember(dest => dest.Relacion, 
                    opt => opt.MapFrom(v => v.Relacion))
                .ForMember(dest => dest.Contacto, 
                    opt => opt.MapFrom(v => v.Contacto));
        }
    }
}