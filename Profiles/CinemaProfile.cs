using AutoMapper;
using SistemaAPIFilmes.Data.Dtos;
using SistemaAPIFilmes.Models;

namespace SistemaAPIFilmes.Profiles;

public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        CreateMap<CreateCinemaDto, Cinema>();
        CreateMap<Cinema, ReadCinemaDto>().ForMember(cinemaDto => cinemaDto.Endereco,
                                                    options => options.MapFrom(cinema => cinema.Endereco))
                                          .ForMember(cinemaDto => cinemaDto.Sessoes,
                                                    options => options.MapFrom(cinema => cinema.Sessoes));
        CreateMap<UpdateCinemaDto, Cinema>();
    }
}
