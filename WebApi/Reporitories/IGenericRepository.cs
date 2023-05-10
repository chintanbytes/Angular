using MyShop.WebApi.Helpers;
using MyShop.WebApi.ResourceParameters;

namespace MyShop.WebApi.Repositories;

public interface IGenericRepository<T, TId>
{
    Task<Result<T>> GetByIdAsync(TId id);
    Task<Result<PagedList<T>>> GetAllAsync(BaseResourceParameters parameters);
    Task<Result<T>> CreateAsync(T entity);
    Task<Result<T>> UpdateAsync(TId id, T entity);
    Task<Result<T>> DeleteAsync(TId id);
}