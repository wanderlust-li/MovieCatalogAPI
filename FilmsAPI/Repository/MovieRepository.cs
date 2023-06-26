using FilmsAPI.Data;
using FilmsAPI.Models;
using FilmsAPI.Repository.IRepository;

namespace FilmsAPI.Repository;

public class MovieRepository : Repository<Movie>, IMovieRepository
{
    private readonly ApplicationDbContext _db;

    public MovieRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Movie> UpdateAsync(Movie entity)
    {
        _db.Movies.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}