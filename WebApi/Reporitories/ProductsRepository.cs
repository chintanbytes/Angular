using Angular.DBContext;

namespace Angular.Repositories;

public class ProductsRepository : GenericRepository<Product>
{
    private readonly NorthwindContext dbcontext;

    public ProductsRepository(NorthwindContext Dbcontext, ILogger<Product> logger) : base(Dbcontext, logger)
    {
        dbcontext = Dbcontext;
    }
}