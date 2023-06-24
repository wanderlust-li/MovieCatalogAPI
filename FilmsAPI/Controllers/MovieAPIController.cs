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
    public ActionResult GetMovie(int id)
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
    public ActionResult GetMovies()
    {
        var films = _db.Movies.ToList();
        var result = films.Select(movie => _mapper.Map<MovieDTO>(movie)).ToList();

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }


    [HttpPost]
    public ActionResult CreateMovie([FromBody] CreateMovieDTO createDto)
    {
        if (createDto == null)
            return BadRequest(createDto);
        
        
        if (_db.Movies.FirstOrDefault(u => u.Title.ToLower() == createDto.Title.ToLower()) != null)
        {
            ModelState.AddModelError("ErrorMessages", "Film already exists!");
            return BadRequest(ModelState);
        }
        
        Movie movie = _mapper.Map<Movie>(createDto);
        _db.Movies.Add(movie);
        _db.SaveChanges();

        return Ok(movie);
    }

    // [HttpGet("{id:int}", Name = "DeleteMovie")]
    [HttpDelete("{id:int}", Name = "DeleteMovie")]
    public ActionResult DeleteMovie(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        
        var movie = _db.Movies.FirstOrDefault(u => u.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        _db.Movies.Remove(movie);
        _db.SaveChanges();

        return Ok(movie);
    }
}