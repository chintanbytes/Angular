using Microsoft.AspNetCore.Mvc;
using MyShop.WebApi.DBContext;
using MyShop.WebApi.Models;
using MyShop.WebApi.ResourceParameters;

namespace MyShop.WebApi.Controllers;

public interface IProductsController : IGenericController<ProductDto, int>
{
    Task<ActionResult<IEnumerable<ProductDto>>> GetEntitiesAsync(ProductResourceParameters parameters);
}