using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Models.DTO;
using MovieAPI.Repository.IRepository;

namespace MovieAPI.Controllers.v1;


[Route("api/v{version:apiVersion}/MovieActor")]
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
    
    [HttpGet("{id:int}", Name = "GetMovieActor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> GetMovieActor(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            var movieActor = await _db.GetAsync(u => u.Id == id);
            if (movieActor == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            _response.Result = _mapper.Map<MovieActorDTO>(movieActor);
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<ActionResult<APIResponse>> CreateMovieActor([FromBody] CreateMovieActorDTO createMovieActor)
    {
        try
        {
            if (createMovieActor == null)
            {
                _response.IsSuccess = false;
                return BadRequest(createMovieActor);
            }

            if (await _db.GetAsync(u => u.MovieId == createMovieActor.MovieId) != null)
            {
                _response.IsSuccess = false;
                ModelState.AddModelError("ErrorMessages", "Film already exists!");

                return BadRequest(ModelState);
            }

            MovieActor movieActor = _mapper.Map<MovieActor>(createMovieActor);
            await _db.CreateAsync(movieActor);

            _response.Result = _mapper.Map<MovieActorDTO>(movieActor);
            _response.StatusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetMovieActor", new {id = movieActor.Id},_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
        
    }
    
}