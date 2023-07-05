namespace MovieAPI.Models.DTO;

public class RegisterationRequestDTO
{
    public string UserName { get; set; }
    
    public string Name { get; set; }
    
    public string Password { get; set; }
    
    public string Role { get; set; }
}