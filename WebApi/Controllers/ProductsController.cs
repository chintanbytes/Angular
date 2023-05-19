using MyShop.WebApi.Data;
using MyShop.WebApi.Models;
using MyShop.WebApi.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyShop.WebApi.ResourceParameters;
using System.Text.Json;

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

    [HttpGet]
    [Route("Search")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetEntitiesAsync([FromQuery] ProductResourceParameters parameters)
    {
        var result = await productRepository.GetAllAsync(parameters);

        if (!result.Success)
        {
            return NotFound();
        }

        int? PreviousPage = result.Data.HasPrevious ? result.Data.CurrentPage - 1 : null;
        int? NextPage = result.Data.HasNext ? result.Data.CurrentPage + 1 : null;

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