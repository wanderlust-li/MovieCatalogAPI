using Microsoft.AspNetCore.Identity;

namespace MovieAPI.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
}