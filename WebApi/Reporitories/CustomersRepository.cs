using MyShop.WebApi.Data;
using MyShop.WebApi.Helpers;
using MyShop.WebApi.ResourceParameters;

namespace MyShop.WebApi.Repositories;

public class CustomersRepository : GenericRepository<Customer>, ICustomersRepository
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<ICustomersRepository> logger;

    public CustomersRepository(ApplicationDbContext Dbcontext, ILogger<ICustomersRepository> logger)
    : base(Dbcontext, logger)
    {
        dbContext = Dbcontext;
        this.logger = logger;
    }

    public async Task<Result<PagedList<Customer>>> GetAllAsync(CustomerResourceParameters parameters)
    {
        try
        {
            var collection = dbContext.Customers as IQueryable<Customer>;

            if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                parameters.SearchQuery = parameters.SearchQuery?.Trim();

                collection = collection.Where(p => p.ApplicationUser.FirstName.Contains(parameters.SearchQuery!));
            }

            if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
            {
                parameters.OrderBy = parameters.OrderBy.Trim();
                if (parameters.OrderBy.Equals("Name"))
                    collection = collection.OrderBy(p => p.ApplicationUser.FirstName).ThenBy(p => p.ApplicationUser.LastName);
            }


            var entity = await PagedList<Customer>.CreateAsync(collection, parameters.PageNumber, parameters.PageSize);

            foreach (var item in entity)
            {
                var x = item.ApplicationUser;
                var y = item.Address;
                var z = item.PhoneNumber;
            }
            return Result<PagedList<Customer>>.SetSuccess(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving a resource {type}.", typeof(Customer).Name);
            return Result<PagedList<Customer>>.SetFailure("Error retrieving a resource.");
        }
    }
}