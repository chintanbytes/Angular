using MyShop.WebApi.Data;
using MyShop.WebApi.Helpers;
using MyShop.WebApi.ResourceParameters;

namespace MyShop.WebApi.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<Result<T>> GetByIdAsync(long id);
    Task<Result<PagedList<T>>> GetAllAsync(BaseResourceParameters parameters);
    Task<Result<T>> CreateAsync(T entity);
    Task<Result<T>> UpdateAsync(long id, T entity);
    Task<Result<T>> DeleteAsync(long id);
}