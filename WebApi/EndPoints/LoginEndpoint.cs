using Microsoft.AspNetCore.Identity;

namespace WebApi.Endpoints;
public class LoginEndpoint
{
    public class LoginForm
    {

        public string username { get; set; }
        public string password { get; set; }
    }

    public static async Task<IResult> Handler(LoginForm form, SignInManager<IdentityUser> signInManager)
    {

        var result = await signInManager.PasswordSignInAsync(form.username, form.password, true, false);

        if (result.Succeeded)
        {
            return Results.Ok();
        }

        return Results.BadRequest();
    }
}