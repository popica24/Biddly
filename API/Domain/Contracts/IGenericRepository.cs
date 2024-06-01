namespace Business.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<bool> AddAsync(T entity);

    Task<IEnumerable<T>> GetByColumnAsync(string columnName, string columnValue, params string[] selectValue);

    Task<bool> UpdateAsync (T entity, params string[] columnsToUpdate);
}
