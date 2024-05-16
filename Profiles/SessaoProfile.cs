using AutoMapper;
using SistemaAPIFilmes.Data.Dtos;
using SistemaAPIFilmes.Models;

namespace SistemaAPIFilmes.Profiles;

public class SessaoProfile : Profile
{
    public SessaoProfile()
    {
        CreateMap<CreateSessaoDto, Sessao>();
        CreateMap<Sessao, ReadSessaoDto>();
    }
}
