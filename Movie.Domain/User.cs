﻿namespace Movie.Domain;

public class User
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public DateTime Dob { get; set; }
    
    // Is it possible to use a specific data type for geolocation instead of a string?
    public string Location { get; set; } 
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}