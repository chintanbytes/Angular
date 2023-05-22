using MyShop.WebApi.Data;
using MyShop.WebApi.Dtos;
using MyShop.WebApi.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyShop.WebApi.ResourceParameters;
using System.Text.Json;
using MyShop.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MyShop.WebApi.Controllers;


public class CustomersController : GenericController<CustomerDto, Customer>, ICustomersController

{
    private readonly ILogger<ICustomersController> logger;
    private readonly ICustomersRepository customerRepository;
    private readonly IMapper mapper;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;

    public CustomersController(ILogger<ICustomersController> logger,
    ICustomersRepository customerRepository,
    IMapper mapper,
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager)
    : base(logger, customerRepository, mapper)
    {
        this.logger = logger;
        this.customerRepository = customerRepository;
        this.mapper = mapper;
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    /// <summary>
    /// Get all entities of type Customer
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetCustomers")]
    [HttpHead]
    [Authorize]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllAsync([FromQuery] CustomerResourceParameters parameters)
    {
        var result = await customerRepository.GetAllAsync(parameters);

        if (!result.Success)
        {
            return NotFound();
        }

        var PreviousPage = result.Data.HasPrevious ? createResourceUri(parameters, ResourceUriType.PreviousPage, "GetCustomers") : null;
        var NextPage = result.Data.HasNext ? createResourceUri(parameters, ResourceUriType.NextPage, "GetCustomers") : null;

        var paginationMetadata = new
        {
            TotalCount = result.Data.TotalCount,
            PageSize = result.Data.PageSize,
            CurrentPage = result.Data.CurrentPage,
            TotalPages = result.Data.TotalPages,
            PreviousPage = PreviousPage,
            NextPage = NextPage
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        var dto = mapper.Map<IEnumerable<CustomerDto>>(result.Data);
        return Ok(dto);

    }

    /// <summary>
    /// Create entity of type T
    /// </summary>
    /// <param name="entity">T</param>
    /// <returns>T</returns>
    [HttpPost(Name = "CreateCustomer")]
    public override async Task<IActionResult> CreateAsync([FromBody] CustomerDto entity)
    {
        if (entity.Password != entity.ConfirmPassword)
        {
            return BadRequest();
        }

        var customer = mapper.Map<Customer>(entity);

        //new
        var createResult = await userManager.CreateAsync(customer.ApplicationUser, entity.Password);

        if (!createResult.Succeeded)
        {
            return BadRequest();
        }
        //customer.ApplicationUser.Id
        var result = await customerRepository.CreateAsync(customer);
        if (result.Success)
        {
            var dto = mapper.Map<CustomerDto>(result.Data);
            return CreatedAtAction("CreateCustomer", new { id = result.Data.Id }, dto);
        }

        return BadRequest();
    }
}