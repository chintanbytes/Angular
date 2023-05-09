using MyShop.WebApi.DBContext;

namespace MyShop.WebApi.Repositories;

public class ProductsRepository : GenericRepository<Product, int>, IProductsRepository
{
    private readonly ApplicationDbContext dbcontext;
    private readonly ILogger<IProductsRepository> logger;

    public ProductsRepository(ApplicationDbContext Dbcontext, ILogger<IProductsRepository> logger)
    : base(Dbcontext, logger)
    {
        dbcontext = Dbcontext;
        this.logger = logger;
    }
}