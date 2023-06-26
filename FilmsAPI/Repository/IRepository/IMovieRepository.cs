using FilmsAPI.Models;

namespace FilmsAPI.Repository.IRepository;

public interface IMovieRepository: IRepository<Movie>
{
    Task<Movie> UpdateAsync(Movie entity);
}