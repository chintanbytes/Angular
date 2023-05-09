using MyShop.WebApi.DBContext;
using MyShop.WebApi.Models;
using MyShop.WebApi.Repositories;
using AutoMapper;

namespace MyShop.WebApi.Controllers;

public class CustomersController : GenericController<CustomerDto, Customer, string>, ICustomersController

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