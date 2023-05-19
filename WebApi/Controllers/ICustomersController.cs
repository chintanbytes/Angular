using Microsoft.AspNetCore.Mvc;
using MyShop.WebApi.Models;
using MyShop.WebApi.ResourceParameters;

namespace MyShop.WebApi.Controllers;

public interface ICustomersController : IGenericController<CustomerDto>
{
    Task<ActionResult<IEnumerable<CustomerDto>>> GetEntitiesAsync(CustomerResourceParameters parameters);

}