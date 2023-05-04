using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public
class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> logger;
    private readonly SignInManager<IdentityUser> signInManager;
    private readonly UserManager<IdentityUser> userManager;

    public UsersController(ILogger<UsersController> logger,
    SignInManager<IdentityUser> signInManager,
    UserManager<IdentityUser> userManager)
    {
        this.logger = logger;
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    //Create login end point
    [HttpPost]
    [Route("login")]
    public async Task<IResult> LoginAsync([FromBody] UserInfo userInfo)
    {
        if (ModelState.IsValid == false)
        {
            return Results.BadRequest();
        }

        var result = await signInManager.PasswordSignInAsync(userInfo.Username, userInfo.Password, true, false);

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
    public async Task<IResult> RegisterAsync([FromBody] NewUserInfo newUserInfo)
    {
        if (newUserInfo.Password != newUserInfo.ConfirmPassword)
        {
            return Results.BadRequest();
        }

        IdentityUser user = new() { UserName = newUserInfo.Username };

        var createResult = await userManager.CreateAsync(user, newUserInfo.Password);

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