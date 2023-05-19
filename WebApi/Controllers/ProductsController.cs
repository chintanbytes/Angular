using MyShop.WebApi.Data;
using MyShop.WebApi.Models;
using MyShop.WebApi.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyShop.WebApi.ResourceParameters;
using System.Text.Json;
using MyShop.WebApi.Helpers;

namespace MyShop.WebApi.Controllers;

public class ProductsController : GenericController<ProductDto, Product>, IProductsController
{
    private readonly ILogger<IProductsController> logger;
    private readonly IProductsRepository productRepository;
    private readonly IMapper mapper;

    public ProductsController(ILogger<IProductsController> logger, IProductsRepository ProductRepository, IMapper mapper)
    : base(logger, ProductRepository, mapper)
    {
        this.logger = logger;
        productRepository = ProductRepository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get all entities of type Product
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HttpHead]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetEntitiesAsync([FromQuery] ProductResourceParameters parameters)
    {
        var result = await productRepository.GetAllAsync(parameters);

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

        var dto = mapper.Map<IEnumerable<ProductDto>>(result.Data);
        return Ok(dto);
    }

    private string? createEntitiesResourceUri(ProductResourceParameters parameters, ResourceUriType uriType)
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