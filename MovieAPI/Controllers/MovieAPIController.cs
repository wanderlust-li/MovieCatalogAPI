using System.Net;
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
    protected APIResponse _response;
    private readonly IMovieRepository _db;
    private readonly IMapper _mapper;

    public MovieAPIController(IMovieRepository db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
        this._response = new();
    }

    [HttpGet("{id:int}", Name = "GetMovie")]
    public async Task<ActionResult<APIResponse>> GetMovie(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var film = await _db.GetAsync(u => u.Id == id);
            if (film == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
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

    [HttpGet]
    public async Task<ActionResult<APIResponse>> GetMovies()
    {
        try
        {
            IEnumerable<Movie> films = await _db.GetAllAsync();

            if (films == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
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


    [HttpPost]
    public async Task<ActionResult<APIResponse>> CreateMovie([FromBody] CreateMovieDTO createDto)
    {
        try
        {
            if (createDto == null)
            {
                return BadRequest(createDto);
            }

            if (await _db.GetAsync(u => u.Title.ToLower() == createDto.Title.ToLower()) != null)
            {
                ModelState.AddModelError("ErrorMessages", "Film already exists!");

                return BadRequest(ModelState);
            }

            Movie movie = _mapper.Map<Movie>(createDto);
            await _db.CreateAsync(movie);

            _response.Result = _mapper.Map<List<MovieDTO>>(movie);
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
    public async Task<ActionResult<APIResponse>> DeleteMovie(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest();
            }

            var movie = await _db.GetAsync(u => u.Id == id);
            if (movie == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
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
    // public async Task<ActionResult> UpdateMovie(int id, [FromBody] UpdateMovieDTO updateDTO)
    // {
    //     if (updateDTO == null || updateDTO.Id != id)
    //     {
    //         return BadRequest();
    //     }
    //
    //     Movie movie = _mapper.Map<Movie>(updateDTO);
    //     await _db.UpdateAsync(movie);
    //
    //     return Ok(movie);
    // }
}