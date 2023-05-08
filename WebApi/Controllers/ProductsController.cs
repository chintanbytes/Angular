
using Angular.DBContext;
using Angular.Models;
using Angular.Repositories;
using AutoMapper;

namespace Angular.Controllers;

public class ProductsController : GenericController<ProductDto, Product>, IProductController

{
    private readonly ILogger<IProductController> logger;
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public ProductsController(ILogger<IProductController> logger, IProductRepository ProductRepository, IMapper mapper)
    : base(logger, ProductRepository, mapper)
    {
        this.logger = logger;
        productRepository = ProductRepository;
        this.mapper = mapper;
    }

    protected override int GetId(Product entity) => entity.ProductId;
}