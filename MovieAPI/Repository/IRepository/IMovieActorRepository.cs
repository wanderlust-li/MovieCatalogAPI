using MovieAPI.Models;

namespace MovieAPI.Repository.IRepository;

public interface IMovieActorRepository : IRepository<MovieActor>
{
    Task<MovieActor> UpdateAsync(MovieActor entity);
}