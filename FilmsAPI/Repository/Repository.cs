using System.Linq.Expressions;
using FilmsAPI.Data;
using Microsoft.EntityFrameworkCore;
using FilmsAPI.Repository.IRepository;

namespace FilmsAPI.Repository;

public class Repository<T>: IRepository<T> where T: class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;
    
    public Repository(ApplicationDbContext db)
    {
        _db = db;
        this.dbSet = _db.Set<T>();
    }
    
    
    public Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}