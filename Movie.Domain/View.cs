using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Domain;

public class View
{
    public Guid Id { get; set; }
    
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public User User { get; set; }  
    
    [ForeignKey("Video")]
    public Guid VideoId { get; set; }
    public Video Video { get; set; }  
    
    public int Offset { get; set; }
    
    public string Platform { get; set; } 
    
    public DateTime CreatedAt { get; set; }
}