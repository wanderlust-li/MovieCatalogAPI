using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;

namespace MovieAPI.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Movie> Movies { get; set; }
    
    public DbSet<LocalUser> LocalUsers { get; set; }
    
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    
    public DbSet<Actor> Actors { get; set; }
    
    public DbSet<MovieActor> MovieActors { get; set; }
}