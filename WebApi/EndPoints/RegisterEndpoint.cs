using Microsoft.AspNetCore.Identity;

namespace WebApi.Endpoints;
public class RegisterEndpoint
{
    public class RegisterForm
    {

        public string username { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
    }

    public static async Task<IResult> Handler(RegisterForm form,
    SignInManager<IdentityUser> signInManager,
    UserManager<IdentityUser> userManager)
    {
        if (form.password != form.confirmpassword)
        {
            return Results.BadRequest();
        }

        IdentityUser user = new() { UserName = form.username };

        var createResult = await userManager.CreateAsync(user, form.password);

        if (!createResult.Succeeded)
        {
            return Results.BadRequest();
        }

        await signInManager.SignInAsync(user, true);

        return Results.Ok();
    }
}