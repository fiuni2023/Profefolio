using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using profefolio.Models.DTOs.Admin;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers
{
    public class AdminMapper : Profile
    {
        public AdminMapper()
        {
            CreateMap<Persona, AdminSinColegioDTO>()
                .ForMember(dest => dest.Id, 
                    opt => opt.MapFrom(v => v.Id))
                .ForMember(dest => dest.Nombre,
                    opt => opt.MapFrom(v => v.Nombre))
                .ForMember(dest => dest.Apellido,
                    opt => opt.MapFrom(v => v.Apellido));
        }
    }
}