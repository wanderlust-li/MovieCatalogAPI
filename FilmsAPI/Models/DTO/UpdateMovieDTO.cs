using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Models.DTO;

public class UpdateMovieDTO
{
    public int Id { get; set; }

    [Range(0, 10, ErrorMessage = "The rating must be between 0 and 10.")]
    public double Rating { get; set; }
}