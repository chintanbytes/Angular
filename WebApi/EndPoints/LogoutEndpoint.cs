using Microsoft.AspNetCore.Identity;

namespace WebApi.Endpoints;
public class LogoutEndpoint
{
    public static async Task<IResult> Handler(SignInManager<IdentityUser> signInManager)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }

}