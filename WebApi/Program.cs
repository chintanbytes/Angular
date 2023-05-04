using System.Reflection;
using Angular.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My Test API",
        Version = "v1",
        Description = "My Test Api"
    });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    //options.OperationFilter<ReplaceEntityTypeNameFilter>();
});

builder.Services.AddDbContext<NorthwindContext>(c => c.UseSqlServer(
    connectionString: builder.Configuration.GetConnectionString("Default")
));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(o =>
{
    o.Password.RequireDigit = false;
    o.Password.RequireLowercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequireUppercase = false;
    o.Password.RequiredLength = 6;
    o.Password.RequiredUniqueChars = 0;
    o.User.RequireUniqueEmail = false;
})
    .AddEntityFrameworkStores<NorthwindContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();
var app = builder.BuildWithSpa();

app.Run();