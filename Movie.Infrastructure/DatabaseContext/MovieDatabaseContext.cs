using Microsoft.EntityFrameworkCore;
using Movie.Domain;

namespace Movie.Infrastructure.DatabaseContext;

public class MovieDatabaseContext : DbContext
{
    public MovieDatabaseContext(DbContextOptions<MovieDatabaseContext> options) : base(options)
    {
        
    }
    
    public DbSet<Tag> Tags { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Video> Videos { get; set; }
    
    public DbSet<View> Views { get; set; }
}