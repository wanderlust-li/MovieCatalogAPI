using AutoMapper;
using MovieAPI.Models;
using MovieAPI.Models.DTO;

namespace MovieAPI;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Movie, MovieDTO>();
        CreateMap<MovieDTO, Movie>();
        
        CreateMap<Movie, CreateMovieDTO>().ReverseMap();
        CreateMap<Movie, UpdateMovieDTO>().ReverseMap();
    }
}