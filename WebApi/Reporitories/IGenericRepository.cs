namespace MyShop.WebApi.Repositories;

public interface IGenericRepository<T, TId>
{
    Task<Result<T>> GetByIdAsync(TId id);
    Task<Result<IEnumerable<T>>> GetAllAsync();
    Task<Result<T>> CreateAsync(T entity);
    Task<Result<T>> UpdateAsync(TId id, T entity);
    Task<Result<T>> DeleteAsync(TId id);
}