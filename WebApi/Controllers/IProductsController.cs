using Microsoft.AspNetCore.Mvc;
using MyShop.WebApi.Data;
using MyShop.WebApi.Dtos;
using MyShop.WebApi.ResourceParameters;

namespace MyShop.WebApi.Controllers;

public interface IProductsController : IGenericController<ProductDto>
{
    Task<ActionResult<IEnumerable<ProductDto>>> GetAllAsync(ProductResourceParameters parameters);
}