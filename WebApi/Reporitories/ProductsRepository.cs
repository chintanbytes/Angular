using Angular.DBContext;

namespace Angular.Repositories;

public class ProductsRepository : GenericRepository<Product>, IProductsRepository
{
    private readonly NorthwindContext dbcontext;
    private readonly ILogger<IProductsRepository> logger;

    public ProductsRepository(NorthwindContext Dbcontext, ILogger<IProductsRepository> logger)
    : base(Dbcontext, logger)
    {
        dbcontext = Dbcontext;
        this.logger = logger;
    }
}