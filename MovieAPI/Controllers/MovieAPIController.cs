using AutoMapper;
using MovieAPI.Data;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Models.DTO;
using MovieAPI.Repository.IRepository;

namespace MovieAPI.Controllers;

[Route("api/Movie")]
[ApiController]
public class MovieAPIController : Controller
{
    private readonly IMovieRepository _db;
    private readonly IMapper _mapper;

    public MovieAPIController(IMovieRepository db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet("{id:int}", Name = "GetMovie")]
    public async Task<ActionResult> GetMovie(int id)
    {
        if (id == 0)
            return BadRequest();
        var film = await _db.GetAsync(u => u.Id == id);
        if (film == null)
            return NotFound();

        var result = _mapper.Map<MovieDTO>(film);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetMovies()
    {
        // var films = _db.Movies.ToList();
        IEnumerable<Movie> films = await _db.GetAllAsync();
        var result = films.Select(movie => _mapper.Map<MovieDTO>(movie)).ToList();

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }


    [HttpPost]
    public async Task<ActionResult> CreateMovie([FromBody] CreateMovieDTO createDto)
    {
        if (createDto == null)
            return BadRequest(createDto);
        
        
        if (_db.GetAsync(u => u.Title.ToLower() == createDto.Title.ToLower()) != null)
        {
            ModelState.AddModelError("ErrorMessages", "Film already exists!");
            return BadRequest(ModelState);
        }
        
        Movie movie = _mapper.Map<Movie>(createDto);
        await _db.CreateAsync(movie);

        return Ok(movie);
    }
    
    [HttpDelete("{id:int}", Name = "DeleteMovie")]
    public async Task<ActionResult> DeleteMovie(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        
        var movie = await _db.GetAsync(u => u.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        await _db.RemoveAsync(movie);

        return Ok(movie);
    }

    [HttpPut("{id:int}", Name = "UpdateMovie")]
    public async Task<ActionResult> UpdateMovie(int id, [FromBody] UpdateMovieDTO updateDTO)
    {
        if (updateDTO == null || updateDTO.Id != id)
        {
            return BadRequest();
        }

        Movie movie = _mapper.Map<Movie>(updateDTO);
        await _db.UpdateAsync(movie);

        return Ok(movie);
    }
}