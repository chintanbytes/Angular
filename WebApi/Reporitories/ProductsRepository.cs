using MyShop.WebApi.DBContext;
using MyShop.WebApi.Helpers;
using MyShop.WebApi.ResourceParameters;

namespace MyShop.WebApi.Repositories;

public class ProductsRepository : GenericRepository<Product, int>, IProductsRepository
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<IProductsRepository> logger;

    public ProductsRepository(ApplicationDbContext dbContext, ILogger<IProductsRepository> logger)
    : base(dbContext, logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<Result<PagedList<Product>>> GetAllAsync(ProductResourceParameters parameters)
    {
        try
        {
            var collection = dbContext.Products as IQueryable<Product>;

            if (!string.IsNullOrWhiteSpace(parameters.CategoryId))
            {
                int categoryIdInt;
                if (int.TryParse(parameters.CategoryId?.Trim(), out categoryIdInt))
                    collection = collection.Where(p => p.CategoryId == categoryIdInt);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                parameters.SearchQuery = parameters.SearchQuery?.Trim();

                collection = collection.Where(p => p.ProductName.Contains(parameters.SearchQuery!));
            }

            var entity = await PagedList<Product>.CreateAsync(collection, parameters.PageNumber, parameters.PageSize);

            return Result<PagedList<Product>>.SetSuccess(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving a resource {type}.", typeof(Product).Name);
            return Result<PagedList<Product>>.SetFailure("Error retrieving a resource.");
        }
    }
}