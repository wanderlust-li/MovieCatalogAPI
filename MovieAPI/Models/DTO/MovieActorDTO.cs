namespace MovieAPI.Models.DTO;

public class MovieActorDTO
{
    public int Id { get; set; }
    
    public int MovieId { get; set; }
    
    public string MovieTitle { get; set; } 
    
    public int ActorId { get; set; }
    
    public string ActorName { get; set; }
}