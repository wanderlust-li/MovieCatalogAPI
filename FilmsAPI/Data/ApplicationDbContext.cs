using FilmsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmsAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Movie> Movies { get; set; }
}