
using MyShop.WebApi.DBContext;
using MyShop.WebApi.Models;
using MyShop.WebApi.Repositories;
using AutoMapper;

namespace MyShop.WebApi.Controllers;

public class ProductsController : GenericController<ProductDto, Product, int>, IProductsController

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

    protected override int GetId(Product entity) => entity.ProductId;
}