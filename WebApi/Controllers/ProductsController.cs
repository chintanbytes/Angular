
using Angular.DBContext;
using Angular.Models;
using Angular.Repositories;
using AutoMapper;

namespace Angular.Controllers;

public class ProductsController : GenericController<ProductDto, Product>
{
    public ProductsController(ILogger<Product> logger, GenericRepository<Product> ProductRepository, IMapper mapper)
    : base(logger, ProductRepository, mapper)
    {

    }

    protected override int GetId(Product entity) => entity.ProductId;
}