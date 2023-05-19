using MyShop.WebApi.Data;
using MyShop.WebApi.Helpers;
using MyShop.WebApi.ResourceParameters;

namespace MyShop.WebApi.Repositories;

public interface ICustomersRepository : IGenericRepository<Customer>
{
    Task<Result<PagedList<Customer>>> GetAllAsync(CustomerResourceParameters parameters);
}