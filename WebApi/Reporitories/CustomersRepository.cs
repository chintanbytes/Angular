using Angular.DBContext;

namespace Angular.Repositories;

public class CustomersRepository : GenericRepository<Customer>, ICustomersRepository
{
    private readonly NorthwindContext dbcontext;
    private readonly ILogger<ICustomersRepository> logger;

    public CustomersRepository(NorthwindContext Dbcontext, ILogger<ICustomersRepository> logger)
    : base(Dbcontext, logger)
    {
        dbcontext = Dbcontext;
        this.logger = logger;
    }
}