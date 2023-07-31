using System.Net;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Models.DTO;
using MovieAPI.Repository.IRepository;

namespace MovieAPI.Controllers;

[Route("api/v{version:apiVersion}/UsersAuth")]
[ApiController]
[ApiVersionNeutral]

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    protected APIResponse _response;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        this._response = new();
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
    {
        var loginRepsonse = await _userRepository.Login(model);
        if (loginRepsonse.User == null || string.IsNullOrEmpty(loginRepsonse.Token))
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Username or password is incorrect");
            return BadRequest(_response);
        }

        _response.StatusCode = HttpStatusCode.OK;
        _response.IsSuccess = true;
        _response.Result = loginRepsonse;

        return Ok(_response);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO model)
    {
        bool ifUserNameUnique = _userRepository.IsUniqueUser(model.UserName);
        if (!ifUserNameUnique)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Username already exists");
            return BadRequest(_response);
        }

        var user = await _userRepository.Register(model);
        if (user == null)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Error while registering");
            return BadRequest(_response);
        }
        _response.StatusCode = HttpStatusCode.OK;
        _response.IsSuccess = true;
        return Ok(_response);
    }
    
}