using MyShop.WebApi.DBContext;

namespace MyShop.WebApi.Repositories;

public interface ICustomersRepository : IGenericRepository<Customer, string>
{
}