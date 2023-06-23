using AutoMapper;
using FilmsAPI.Models;
using FilmsAPI.Models.DTO;

namespace FilmsAPI;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Movie, MovieDTO>();
        CreateMap<MovieDTO, Movie>();
        
        CreateMap<Movie, CreateMovieDTO>().ReverseMap();
    }
}