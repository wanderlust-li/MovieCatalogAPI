using System.ComponentModel.DataAnnotations.Schema;
using Movie.Domain.Common;

namespace Movie.Domain;

public class View : BaseEntity
{
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public User User { get; set; }  
    
    [ForeignKey("Video")]
    public Guid VideoId { get; set; }
    public Video Video { get; set; }  
    
    public int Offset { get; set; }
    
    public string Platform { get; set; }
}