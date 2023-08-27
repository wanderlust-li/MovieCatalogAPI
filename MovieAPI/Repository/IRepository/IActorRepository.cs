using MovieAPI.Models;

namespace MovieAPI.Repository.IRepository;

public interface IActorRepository : IRepository<Actor>
{
    Task<Actor> UpdateAsync(Actor entity);
}