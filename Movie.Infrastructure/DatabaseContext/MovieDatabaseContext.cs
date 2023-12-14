using Microsoft.EntityFrameworkCore;


namespace Movie.Infrastructure.DatabaseContext;

public class MovieDatabaseContext : DbContext
{
    public MovieDatabaseContext(DbContextOptions<MovieDatabaseContext> options) : base(options)
    {

    }
}