using AutoMapper;
using profefolio.Models.DTOs.Documento;
using profefolio.Models.Entities;

namespace profefolio.Helpers.Mappers;

public class DocumentoMapper : Profile
{
    public DocumentoMapper()
    {
        CreateMap<Documento, DocumentoDTO>();

        CreateMap<DocumentoDTO, Documento>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.Created,
                opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Modified,
                opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy,
                opt => opt.Ignore());

        CreateMap<Documento, DocumentoResultDTO>();

        CreateMap<DocumentoResultDTO, Documento>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.Created,
                opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Modified,
                opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy,
                opt => opt.Ignore());
        

       
    }
}