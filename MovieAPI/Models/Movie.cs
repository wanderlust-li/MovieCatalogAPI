using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models;

public class Movie
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "The movie title is required.")]
    [StringLength(100, ErrorMessage = "The movie title must be at most 100 characters.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "The movie description is required.")]
    [StringLength(500, ErrorMessage = "The movie description must be at most 500 characters.")]
    public string Description { get; set; }

    [StringLength(100, ErrorMessage = "The country must be at most 100 characters.")]
    public string Country { get; set; }

    public string ImageUrl { get; set; }

    [Required(ErrorMessage = "The genre is required.")]
    public string Genre { get; set; }

    [Range(0, 500, ErrorMessage = "The duration must be between 0 and 500 minutes.")]
    public double Duration { get; set; }

    [Range(0, 10, ErrorMessage = "The rating must be between 0 and 10.")]
    public double Rating { get; set; }
    
    public ICollection<MovieActor> MovieActors { get; set; }

}