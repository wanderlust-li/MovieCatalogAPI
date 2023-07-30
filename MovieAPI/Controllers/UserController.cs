using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Repository.IRepository;

namespace MovieAPI.Controllers;

[Route("api/v{version:apiVersion}/UsersAuth")]
[ApiController]
[ApiVersionNeutral]

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    protected APIResponse _response;
    
    
}