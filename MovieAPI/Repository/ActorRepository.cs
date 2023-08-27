using MovieAPI.Data;
using MovieAPI.Models;
using MovieAPI.Repository.IRepository;

namespace MovieAPI.Repository;

public class ActorRepository : Repository<Actor>, IActorRepository
{
    private readonly ApplicationDbContext _db;

    public ActorRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Actor> UpdateAsync(Actor entity)
    {
        _db.Actors.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}