namespace Movie.Domain;

public class View
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid VideoId { get; set; }
    
    public int Offset { get; set; }
    
    public string Platform { get; set; } 
    
    public DateTime CreatedAt { get; set; }
}