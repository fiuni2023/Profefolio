using AutoMapper;
using profefolio.Models.DTOs;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers;

public class ProfesorMapper : Profile
{
    public ProfesorMapper()
    {
        CreateMap<ProfesorDTO, Persona>();
    }
}