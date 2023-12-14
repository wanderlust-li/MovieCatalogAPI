using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Movie.Domain.UserEntity;

public class User : IdentityUser
{
    [MaxLength]
    public string UserName { get; set; }
    
    public int Age { get; set; }
    
    public int? SexId { get; set; }
    
    public virtual Sex Sex { get; set; }
}