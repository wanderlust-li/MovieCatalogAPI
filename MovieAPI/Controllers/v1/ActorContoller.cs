using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Models.DTO;
using MovieAPI.Repository.IRepository;

namespace MovieAPI.Controllers.v1;

[Route("api/v{version:apiVersion}/Actor")]
[ApiController]
[ApiVersion("1.0")]
public class ActorContoller : Controller
{
    
    private readonly APIResponse _response;
    private readonly IActorRepository _db;
    private readonly IMapper _mapper;
    
    public ActorContoller(IActorRepository db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
        this._response = new();
    }
   
    
    [HttpGet("{id:int}", Name = "GetActor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> GetActor(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            var actor = await _db.GetAsync(u => u.Id == id);
            if (actor == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            _response.Result = _mapper.Map<ActorDTO>(actor);
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetActors()
    {
        try
        {
            IEnumerable<Actor> actors = await _db.GetAllAsync();

            if (actors == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            
            _response.Result = _mapper.Map<List<ActorDTO>>(actors);
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<ActionResult<APIResponse>> CreateActor([FromBody] ActorCreateDTO actorCreateDto)
    {
        try
        {
            if (actorCreateDto == null)
            {
                _response.IsSuccess = false;
                return BadRequest(actorCreateDto);
            }

            if (await _db.GetAsync(u => u.Name.ToLower() == actorCreateDto.Name.ToLower()) != null)
            {
                _response.IsSuccess = false;
                ModelState.AddModelError("ErrorMessages", "Actor already exists!");

                return BadRequest(ModelState);
            }

            Actor actor = _mapper.Map<Actor>(actorCreateDto);
            await _db.CreateAsync(actor);

            ActorDTO actorDto = _mapper.Map<ActorDTO>(actor);
            _response.Result = new List<ActorDTO> { actorDto };
            
            
            _response.StatusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetActor", new {id = actor.Id},_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
        
    }
    
    [HttpDelete("{id:int}", Name = "DeleteActor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> DeleteActor(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest();
            }

            var actor = await _db.GetAsync(u => u.Id == id);
            if (actor == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound();
            }

            await _db.RemoveAsync(actor);
            
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;

            return Ok(actor);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }
        
        return _response;
    }
    
    
}