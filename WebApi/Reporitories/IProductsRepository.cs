using MyShop.WebApi.DBContext;

namespace MyShop.WebApi.Repositories;

public interface IProductsRepository : IGenericRepository<Product, int>
{
}