using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using MovieAPI.Data;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Models.DTO;
using MovieAPI.Repository.IRepository;

namespace MovieAPI.Controllers.v1;


[Route("api/v{version:apiVersion}/Movie")]
[ApiController]
[ApiVersion("1.0")]
public class MovieAPIController : Controller
{
    private readonly APIResponse _response;
    private readonly IMovieRepository _db;
    private readonly IMapper _mapper;

    public MovieAPIController(IMovieRepository db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
        this._response = new();
    }

    [HttpGet("{id:int}", Name = "GetMovie")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> GetMovie(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            var film = await _db.GetAsync(u => u.Id == id);
            if (film == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            _response.Result = _mapper.Map<MovieDTO>(film);
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

    // [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetMovies()
    {
        try
        {
            IEnumerable<Movie> films = await _db.GetAllAsync();

            if (films == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            
            _response.Result = _mapper.Map<List<MovieDTO>>(films);
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);;
        }
        
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
        
    }

    // [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<ActionResult<APIResponse>> CreateMovie([FromBody] CreateMovieDTO createDto)
    {
        try
        {
            if (createDto == null)
            {
                _response.IsSuccess = false;
                return BadRequest(createDto);
            }

            if (await _db.GetAsync(u => u.Title.ToLower() == createDto.Title.ToLower()) != null)
            {
                _response.IsSuccess = false;
                ModelState.AddModelError("ErrorMessages", "Film already exists!");

                return BadRequest(ModelState);
            }

            Movie movie = _mapper.Map<Movie>(createDto);
            await _db.CreateAsync(movie);

            _response.Result = _mapper.Map<MovieDTO>(movie);
            _response.StatusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetMovie", new {id = movie.Id},_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
        
    }
    
    [HttpDelete("{id:int}", Name = "DeleteMovie")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> DeleteMovie(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest();
            }

            var movie = await _db.GetAsync(u => u.Id == id);
            if (movie == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound();
            }

            await _db.RemoveAsync(movie);
            
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;

            return Ok(movie);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }
        
        return _response;
    }

    // [HttpPut("{id:int}", Name = "UpdateMovie")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult<APIResponse>> UpdateMovie(int id, [FromBody] UpdateMovieDTO updateMovieDTO)
    // {
    //     try
    //     {
    //         if (updateMovieDTO == null || updateMovieDTO.Id != id)
    //         {
    //             _response.StatusCode = HttpStatusCode.BadRequest;
    //             return BadRequest();
    //         }
    //
    //         Movie movie = _mapper.Map<Movie>(updateMovieDTO);
    //         await _db.UpdateAsync(movie);
    //
    //         _response.StatusCode = HttpStatusCode.Created;
    //         _response.IsSuccess = true;
    //
    //         return Ok(movie);
    //     }
    //     catch (Exception ex)
    //     {
    //         _response.IsSuccess = false;
    //         _response.ErrorMessages = new List<string>() { ex.ToString() };
    //     }
    //
    //     return _response;
    // }
    
}