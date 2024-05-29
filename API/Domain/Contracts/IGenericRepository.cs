namespace Business.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<bool> AddAsync(T entity);
    Task<IEnumerable<T>> GetByColumn(string columnName, string columnValue);
}
