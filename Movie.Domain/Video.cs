using System.ComponentModel.DataAnnotations.Schema;
using Movie.Domain.Common;

namespace Movie.Domain;

public class Video : BaseEntity
{
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public User User { get; set; }  
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public string ThumbnailURL { get; set; }
    
    public string StreamURL { get; set; }
    
    public ICollection<Tag> Tags { get; set; }
    
}