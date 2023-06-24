using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Models;

public class Movie
{
    [Key]
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
}