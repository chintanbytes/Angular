
using Angular.DBContext;
using Angular.Models;
using Angular.Repositories;
using AutoMapper;

namespace Angular.Controllers;

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

    protected override string GetId(Product entity) => entity.ProductId.ToString();
}