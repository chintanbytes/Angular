using MyShop.WebApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyShop.WebApi.Data;

namespace MyShop.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> logger;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;

    public UsersController(ILogger<UsersController> logger,
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager)
    {
        this.logger = logger;
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    //Create login end point
    [HttpPost]
    [Route("login")]
    public async Task<IResult> LoginAsync([FromBody] UserLoginDto loginData)
    {
        if (ModelState.IsValid == false)
        {
            return Results.BadRequest();
        }

        var result = await signInManager.PasswordSignInAsync(loginData.Username, loginData.Password, true, false);

        if (result.Succeeded)
        {
            return Results.Ok();
        }

        return Results.BadRequest();
    }

    //Create logout end point
    [HttpGet]
    [Authorize]
    [Route("logout")]
    public async Task<IResult> LogoutAsync()
    {
        await signInManager.SignOutAsync();
        return Results.Ok();

    }

    //Create register end point
    [HttpPost]
    [Route("register")]
    public async Task<IResult> RegisterAsync([FromBody] UserDto userData)
    {
        if (userData.Password != userData.ConfirmPassword)
        {
            return Results.BadRequest();
        }

        ApplicationUser user = new() { UserName = userData.Username };

        var createResult = await userManager.CreateAsync(user, userData.Password);

        if (!createResult.Succeeded)
        {
            return Results.BadRequest();
        }

        await signInManager.SignInAsync(user, true);

        return Results.Ok();
    }

    //Get current user
    [HttpGet]
    public Dictionary<string, string> GetUser()
    {
        return User.Claims.ToDictionary(c => c.Type, c => c.Value);
    }
}