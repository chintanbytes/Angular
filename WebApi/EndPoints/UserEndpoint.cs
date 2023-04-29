using System.Security.Claims;

namespace WebApi.Endpoints;
public class UserEndpoint
{
    public static Dictionary<string, string> Handler(ClaimsPrincipal user) =>
    user.Claims.ToDictionary(c => c.Type, c => c.Value);

}