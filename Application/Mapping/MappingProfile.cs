using AutoMapper;
using Mottu.Application.DTOs;
using Mottu.Domain.Entities;

namespace Mottu.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Moto, MotoDTO>()
                .ForMember(d => d.Placa, o => o.MapFrom(s => s.Placa.Valor))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Setor, o => o.MapFrom(s => s.SetorCor.Setor))
                .ForMember(d => d.Cor, o => o.MapFrom(s => s.SetorCor.Cor))
                .ForMember(d => d.NomePatio, o => o.MapFrom(s => s.Patio.Nome));

            CreateMap<Patio, PatioDTO>().ReverseMap();
            CreateMap<UsuarioPatio, UsuarioPatioDTO>().ReverseMap();
        }
    }
}