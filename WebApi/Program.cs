using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IdentityDb>(c => c.UseSqlServer(
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
    .AddEntityFrameworkStores<IdentityDb>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();

var app = builder.BuildWithSpa();

var apiEndpoints = app.MapGroup("/api");

apiEndpoints.MapGet("/user", UserEndpoint.Handler);

apiEndpoints.MapPost("/login", LoginEndpoint.Handler);

apiEndpoints.MapGet("/logout", LogoutEndpoint.Handler).RequireAuthorization();

apiEndpoints.MapPost("/register", RegisterEndpoint.Handler);

app.Run();