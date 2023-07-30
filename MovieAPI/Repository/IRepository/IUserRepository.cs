using MovieAPI.Models.DTO;

namespace MovieAPI.Repository.IRepository;

public interface IUserRepository
{
    bool IsUniqueUser(string username);

    Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDto);

    Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO);
}