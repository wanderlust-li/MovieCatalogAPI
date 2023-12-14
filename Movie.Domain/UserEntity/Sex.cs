
using System.ComponentModel.DataAnnotations;

namespace Movie.Domain.UserEntity;

public class Sex 
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }

    public ICollection<User> Users { get; set; } = new HashSet<User>();
}