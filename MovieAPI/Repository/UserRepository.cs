using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MovieAPI.Data;
using MovieAPI.Models;
using MovieAPI.Models.DTO;
using MovieAPI.Repository.IRepository;

namespace MovieAPI.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private string secretKey;
    private readonly IMapper _mapper;

    public UserRepository(ApplicationDbContext db, IConfiguration configuration,
        UserManager<ApplicationUser> userManager, IMapper mapper,
        RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _mapper = mapper;
        secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        _roleManager = roleManager;
    }


    public bool IsUniqueUser(string username)
    {
        var user = _db.ApplicationUsers.FirstOrDefault(x => x.UserName == username);
        if (user == null)
            return true;

        return false;
    }

    public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO)
    {
        throw new NotImplementedException();
    }
}