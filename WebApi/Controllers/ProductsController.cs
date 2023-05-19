using MyShop.WebApi.Data;
using MyShop.WebApi.Dtos;
using MyShop.WebApi.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyShop.WebApi.ResourceParameters;
using System.Text.Json;
using MyShop.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace MyShop.WebApi.Controllers;

[Authorize]
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
    [HttpGet(Name = "GetProducts")]
    [HttpHead]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllAsync([FromQuery] ProductResourceParameters parameters)
    {
        var result = await productRepository.GetAllAsync(parameters);

        if (!result.Success)
        {
            return NotFound();
        }

        var PreviousPage = result.Data.HasPrevious ? createResourceUri(parameters, ResourceUriType.PreviousPage, "GetProducts") : null;
        var NextPage = result.Data.HasNext ? createResourceUri(parameters, ResourceUriType.NextPage, "GetProducts") : null;

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
}