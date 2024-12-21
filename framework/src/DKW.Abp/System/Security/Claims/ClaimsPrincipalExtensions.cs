using OpenIddict.Abstractions;

namespace System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static Int32 GetUserId(this ClaimsPrincipal user)
    {
        var userId = user.Claims.FirstOrDefault(c => c.Type == OpenIddictConstants.Claims.Subject)!.Value;
        return Int32.Parse(userId);
    }

    public static String GetUserName(this ClaimsPrincipal user)
        => user.Claims.FirstOrDefault(c => c.Type == OpenIddictConstants.Claims.Name)!.Value;

    public static String GetUserEmail(this ClaimsPrincipal user)
        => user.Claims.FirstOrDefault(c => c.Type == OpenIddictConstants.Claims.Email)!.Value;
}
