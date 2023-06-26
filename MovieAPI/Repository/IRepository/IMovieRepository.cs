using MovieAPI.Models;

namespace MovieAPI.Repository.IRepository;

public interface IMovieRepository: IRepository<Movie>
{
    Task<Movie> UpdateAsync(Movie entity);
}