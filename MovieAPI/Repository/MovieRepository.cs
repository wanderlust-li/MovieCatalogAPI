using MovieAPI.Data;
using MovieAPI.Models;
using MovieAPI.Repository.IRepository;

namespace MovieAPI.Repository;

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