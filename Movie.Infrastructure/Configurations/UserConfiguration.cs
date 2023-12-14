using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.UserEntity;

namespace Movie.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var admin = new User
        {
            Id = "B22698B8-42A2-4115-9631-1C2D1E2AC5F7",
            UserName = "Yevhenii",
            NormalizedUserName = "YEHVENII",
            Email = "yevhenii@gmail.com",
            NormalizedEmail = "@YEVHENII@GMAIL.COM",
            EmailConfirmed = true,
            Age = 19,
            SexId = 1
        };
        admin.PasswordHash = PassGenerate(admin);
        builder.HasData(admin);
        
    }
    
    public string PassGenerate(IdentityUser user)
    {
        var passHash = new PasswordHasher<IdentityUser>();
        return passHash.HashPassword(user, "YevheniiLi");
    }
}