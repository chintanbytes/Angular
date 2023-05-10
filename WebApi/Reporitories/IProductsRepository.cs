using MyShop.WebApi.DBContext;
using MyShop.WebApi.Helpers;
using MyShop.WebApi.ResourceParameters;

namespace MyShop.WebApi.Repositories;

public interface IProductsRepository : IGenericRepository<Product, int>
{
    Task<Result<PagedList<Product>>> GetAllAsync(ProductResourceParameters parameters);

}