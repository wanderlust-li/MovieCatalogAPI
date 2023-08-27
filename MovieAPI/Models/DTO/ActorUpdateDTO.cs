using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models.DTO;

public class ActorUpdateDTO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}