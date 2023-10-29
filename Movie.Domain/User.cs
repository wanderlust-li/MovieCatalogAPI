using Movie.Domain.Common;

namespace Movie.Domain;

public class User : BaseEntity
{
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public DateTime Dob { get; set; }
    
    // Is it possible to use a specific data type for geolocation instead of a string?
    public string Location { get; set; } 
    
}