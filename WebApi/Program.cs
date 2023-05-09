using System.Reflection;
using MyShop.WebApi.DBContext;
using MyShop.WebApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using MyShop.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

builder.Services.AddControllers()
                .AddNewtonsoftJson(actions =>
                {
                    actions.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddXmlDataContractSerializerFormatters();

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