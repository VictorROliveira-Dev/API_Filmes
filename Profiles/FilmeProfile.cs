using AutoMapper;
using SistemaAPIFilmes.Data.Dtos;
using SistemaAPIFilmes.Models;

namespace SistemaAPIFilmes.Profiles;

public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<CreateFilmeDto, Filme>();
        CreateMap<UpdateFilmeDto, Filme>();
        CreateMap<Filme, UpdateFilmeDto>();
        CreateMap<Filme, ReadFilmeDto>();
    }
}
