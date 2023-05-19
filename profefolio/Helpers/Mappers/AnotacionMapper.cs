using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using profefolio.Models.DTOs.Anotacion;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers
{
    public class AnotacionMapper : Profile
    {
        public AnotacionMapper()
        {
            CreateMap<AnotacionCreateDTO, Anotacion>();
        }
    }
}