using MyShop.WebApi.DBContext;
using MyShop.WebApi.Models;

namespace MyShop.WebApi.Controllers;

public interface IProductsController : IGenericController<ProductDto, int>
{

}