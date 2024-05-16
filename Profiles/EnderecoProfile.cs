using AutoMapper;
using SistemaAPIFilmes.Data.Dtos;
using SistemaAPIFilmes.Models;

namespace SistemaAPIFilmes.Profiles;

public class EnderecoProfile : Profile
{
    public EnderecoProfile()
    {
        CreateMap<CreateEnderecoDto, Endereco>();
        CreateMap<Endereco, ReadEnderecoDto>();
        CreateMap<UpdateEnderecoDto, Endereco>();
    }
}
