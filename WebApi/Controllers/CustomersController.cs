using Angular.DBContext;
using Angular.Models;
using Angular.Repositories;
using AutoMapper;

namespace Angular.Controllers;

public class CustomersController : GenericController<CustomerDto, Customer>, ICustomersController

{
    private readonly ILogger<ICustomersController> logger;
    private readonly ICustomersRepository customerRepository;
    private readonly IMapper mapper;

    public CustomersController(ILogger<ICustomersController> logger, ICustomersRepository customerRepository, IMapper mapper)
    : base(logger, customerRepository, mapper)
    {
        this.logger = logger;
        this.customerRepository = customerRepository;
        this.mapper = mapper;
    }

    protected override string GetId(Customer entity) => entity.CustomerId;
}