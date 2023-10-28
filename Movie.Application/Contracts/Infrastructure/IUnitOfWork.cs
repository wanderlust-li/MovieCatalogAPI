namespace Movie.Application.Contracts.Infrastructure;

public interface IUnitOfWork<T> where T: class
{
    Task<T> GetAsync();
    
    Task<T> GetByIdAsync(int id);

    Task CreateAsync(T entity);
    
    Task UpdateAsync(T entity);
    
    Task DeleteAsync(T entity);
}