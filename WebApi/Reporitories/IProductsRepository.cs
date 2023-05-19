using MyShop.WebApi.Data;
using MyShop.WebApi.Helpers;
using MyShop.WebApi.ResourceParameters;

namespace MyShop.WebApi.Repositories;

public interface IProductsRepository : IGenericRepository<Product>
{
    Task<Result<PagedList<Product>>> GetAllAsync(ProductResourceParameters parameters);

}