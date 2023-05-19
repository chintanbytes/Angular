using Microsoft.AspNetCore.Mvc;
using MyShop.WebApi.Dtos;
using MyShop.WebApi.ResourceParameters;

namespace MyShop.WebApi.Controllers;

public interface ICustomersController : IGenericController<CustomerDto>
{
    Task<ActionResult<IEnumerable<CustomerDto>>> GetAllAsync(CustomerResourceParameters parameters);

}