using AutoMapper;
using Mottu.Application.DTOs;
using Mottu.Domain.Entities;

namespace Mottu.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamentos para Moto
            CreateMap<Moto, MotoDto>()
                .ForMember(d => d.Placa, o => o.MapFrom(s => s.Placa.Valor));

            CreateMap<Moto, MotoResponseDto>()
                .ForMember(d => d.Placa, o => o.MapFrom(s => s.Placa.Valor))
                .ForMember(d => d.Setor, o => o.MapFrom(s => s.SetorCor.Setor))
                .ForMember(d => d.CorSetor, o => o.MapFrom(s => s.SetorCor.Cor))
                .ForMember(d => d.NomePatio, o => o.MapFrom(s => s.Patio.Nome));

            CreateMap<MotoDto, Moto>()
                .ForMember(d => d.Placa, o => o.MapFrom(s => new Domain.ValueObjects.Placa(s.Placa)))
                .ForMember(d => d.Patio, o => o.Ignore())
                .ForMember(d => d.SetorCor, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore());

            // Mapeamentos para Patio
            CreateMap<Patio, PatioDto>().ReverseMap();

            // Mapeamentos para Usuario
            CreateMap<Usuario, UsuarioDto>().ReverseMap();

            // Mapeamentos para UsuarioPatio (mantido para compatibilidade)
            CreateMap<UsuarioPatio, UsuarioPatioDTO>().ReverseMap();
        }
    }
}