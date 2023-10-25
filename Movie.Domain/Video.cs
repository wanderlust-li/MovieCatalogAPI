namespace Movie.Domain;

public class Video
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public string ThumbnailURL { get; set; }
    
    public string StreamURL { get; set; }
    
    public Guid? Tags { get; set; } 
    
    public DateTime UploadedAt { get; set; }
}