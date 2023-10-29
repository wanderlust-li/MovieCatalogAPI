namespace Movie.Application.Models.Identity;

public class RegisterationRequest
{
    public string UserName { get; set; }
    
    public string Name { get; set; }
    
    public string Password { get; set; }
    
    public string Role { get; set; }
}