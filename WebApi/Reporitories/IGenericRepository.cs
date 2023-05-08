namespace Angular.Repositories;

public interface IGenericRepository<T>
{
    Task<Result<T>> GetByIdAsync(int id);
    Task<Result<IEnumerable<T>>> GetAllAsync();
    Task<Result<T>> CreateAsync(T entity);
    Task<Result<T>> UpdateAsync(int id, T entity);
    Task<Result<T>> DeleteAsync(int id);
}