using System.Reflection;
using MyShop.WebApi.Data;
using MyShop.WebApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using MyShop.WebApi.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
// using log4net;
// using log4net.Config;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Read Configuration from appSettings
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

//Configure Serilog
Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

// Configure log4net
// XmlConfigurator.Configure(new FileInfo("log4net.config"));

builder.Services.AddLogging(builder =>
{
    //builder.AddLog4Net(); // Add Log4Net as the logging provider
    builder.AddSerilog(); // Add Serilog as the logging provider
});

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(actions =>
    {
        actions.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    })
    .AddXmlDataContractSerializerFormatters()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var problemDetailFactory = context.HttpContext.RequestServices
            .GetRequiredService<ProblemDetailsFactory>();
            var problemDetails = problemDetailFactory
            .CreateValidationProblemDetails(context.HttpContext, context.ModelState);

            problemDetails.Detail = "Please check the errors list.";
            problemDetails.Instance = context.HttpContext.Request.Path;

            problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
            problemDetails.Title = "One or more validation errors occured";

            return new UnprocessableEntityObjectResult(problemDetails)
            {
                ContentTypes = { "application/problem+json" }
            };
        };
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My Shop API",
        Version = "v1",
        Description = "My Shop Api"
    });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.OperationFilter<ReplaceEntityTypeNameFilter>();
});

builder.Services.AddDbContext<ApplicationDbContext>(c => c.UseSqlServer(
    connectionString: builder.Configuration.GetConnectionString("Default")
));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(o =>
{
    o.Password.RequireDigit = false;
    o.Password.RequireLowercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequireUppercase = false;
    o.Password.RequiredLength = 6;
    o.Password.RequiredUniqueChars = 0;
    o.User.RequireUniqueEmail = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();

var app = builder.BuildWithSpa();
app.Run();