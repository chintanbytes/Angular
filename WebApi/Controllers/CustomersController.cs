using MyShop.WebApi.Data;
using MyShop.WebApi.Models;
using MyShop.WebApi.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyShop.WebApi.ResourceParameters;
using System.Text.Json;
using MyShop.WebApi.Helpers;

namespace MyShop.WebApi.Controllers;

public class CustomersController : GenericController<CustomerDto, Customer>, ICustomersController

{
    private readonly ILogger<ICustomersController> logger;
    private readonly ICustomersRepository customerRepository;
    private readonly IMapper mapper;

    public CustomersController(ILogger<ICustomersController> logger, ICustomersRepository customerRepository, IMapper mapper)
    : base(logger, customerRepository, mapper)
    {
        this.logger = logger;
        this.customerRepository = customerRepository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get all entities of type Customer
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetCustomers")]
    [HttpHead]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetEntitiesAsync([FromQuery] CustomerResourceParameters parameters)
    {
        var result = await customerRepository.GetAllAsync(parameters);

        if (!result.Success)
        {
            return NotFound();
        }

        var PreviousPage = result.Data.HasPrevious ? createEntitiesResourceUri(parameters, ResourceUriType.PreviousPage) : null;
        var NextPage = result.Data.HasNext ? createEntitiesResourceUri(parameters, ResourceUriType.NextPage) : null;

        var paginationMetadata = new
        {
            TotalCount = result.Data.TotalCount,
            PageSize = result.Data.PageSize,
            CurrentPage = result.Data.CurrentPage,
            TotalPages = result.Data.TotalPages,
            PreviousPage = PreviousPage,
            NextPage = NextPage
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        var dto = mapper.Map<IEnumerable<CustomerDto>>(result.Data);
        return Ok(dto);

    }

    private string? createEntitiesResourceUri(CustomerResourceParameters parameters, ResourceUriType uriType)
    {
        switch (uriType)
        {
            case ResourceUriType.PreviousPage:
                return Url.Link("GetCustomers", new { pageNumber = parameters.PageNumber - 1, pageSize = parameters.PageSize });
            case ResourceUriType.NextPage:
                return Url.Link("GetCustomers", new { pageNumber = parameters.PageNumber + 1, pageSize = parameters.PageSize });
            default:
                return Url.Link("GetCustomers", new { pageNumber = parameters.PageNumber, pageSize = parameters.PageSize });
        }
    }
}