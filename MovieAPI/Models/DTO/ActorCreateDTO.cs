using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models.DTO;

public class ActorCreateDTO
{
    [Required]
    public string Name { get; set; }
}