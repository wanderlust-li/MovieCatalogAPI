using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Repository.IRepository;

namespace MovieAPI.Controllers.v1;


[Route("api/v{version:apiVersion}/Actor")]
[ApiController]
[ApiVersion("1.0")]
public class MovieActorController : Controller
{
    private readonly APIResponse _response;
    private readonly IMovieActorRepository _db;
    private readonly IMapper _mapper;

    public MovieActorController(IMovieActorRepository db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
        this._response = new();
    }
    
    
    
}