namespace Business.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<bool> AddAsync(T entity);
}
