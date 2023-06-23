using AutoMapper;
using FilmsAPI.Data;
using FilmsAPI.Models;
using FilmsAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FilmsAPI.Controllers;

[Route("api/Movie")]
[ApiController]
public class MovieAPIController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public MovieAPIController(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet("{id:int}", Name = "GetMovie")]
    public async Task<ActionResult> GetMovie(int id)
    {
        if (id == 0)
            return BadRequest();
        var film = _db.Movies.FirstOrDefault(u => u.Id == id);
        if (film == null)
            return NotFound();

        var result = _mapper.Map<MovieDTO>(film);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetMovies()
    {
        var films = _db.Movies.ToList();
        var result = films.Select(movie => _mapper.Map<MovieDTO>(movie)).ToList();

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);

    }
}