using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using profefolio.Models.DTOs.Asistencia;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers
{
    public class AsistenciaMapper : Profile
    {
        public AsistenciaMapper()
        {

            CreateMap<ClasesAlumnosColegio, AsistenciaResultDTO>()
                .ForMember(dest => dest.Nombre,
                    opt => opt.MapFrom(v => v.ColegiosAlumnos.Persona.Nombre))
                .ForMember(dest => dest.Apellido,
                    opt => opt.MapFrom(v => v.ColegiosAlumnos.Persona.Apellido))
                .ForMember(dest => dest.Documento,
                    opt => opt.MapFrom(v => v.ColegiosAlumnos.Persona.Documento))
                .ForMember(dest => dest.Asistencias,
                    opt => opt.MapFrom(v =>
                        v.Asistencias.OrderBy(a => a.Fecha).TakeWhile(a => a.Fecha > a.Fecha.AddDays(-5)).Select(b => new AssitenciasFechaResult()
                        {
                            Fecha = b.Fecha,
                            Id = b.Id,
                            Estado = b.Estado,
                            Observacion = b.Observacion
                        }))
                );
        }
    }
}