using MovieAPI.Data;
using MovieAPI.Models;
using MovieAPI.Repository.IRepository;

namespace MovieAPI.Repository;

public class MovieActorRepository : Repository<MovieActor>, IMovieActorRepository
{
    private readonly ApplicationDbContext _db;

    public MovieActorRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<MovieActor> UpdateAsync(MovieActor entity)
    {
        _db.MovieActors.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}