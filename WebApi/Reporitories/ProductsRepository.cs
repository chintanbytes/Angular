using Angular.DBContext;

namespace Angular.Repositories;

public class ProductsRepository : GenericRepository<Product>, IProductRepository
{
    private readonly NorthwindContext dbcontext;
    private readonly ILogger<IProductRepository> logger;

    public ProductsRepository(NorthwindContext Dbcontext, ILogger<IProductRepository> logger)
    : base(Dbcontext, logger)
    {
        dbcontext = Dbcontext;
        this.logger = logger;
    }
}