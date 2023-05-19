using Microsoft.AspNetCore.Mvc;
using MyShop.WebApi.Data;
using MyShop.WebApi.Models;
using MyShop.WebApi.ResourceParameters;

namespace MyShop.WebApi.Controllers;

public interface IProductsController : IGenericController<ProductDto>
{
    Task<ActionResult<IEnumerable<ProductDto>>> GetEntitiesAsync(ProductResourceParameters parameters);
}