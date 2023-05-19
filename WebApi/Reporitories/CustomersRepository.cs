using MyShop.WebApi.Data;

namespace MyShop.WebApi.Repositories;

public class CustomersRepository : GenericRepository<Customer>, ICustomersRepository
{
    private readonly ApplicationDbContext dbcontext;
    private readonly ILogger<ICustomersRepository> logger;

    public CustomersRepository(ApplicationDbContext Dbcontext, ILogger<ICustomersRepository> logger)
    : base(Dbcontext, logger)
    {
        dbcontext = Dbcontext;
        this.logger = logger;
    }
}