using System.Security.Claims;
using Volo.Abp.Security.Claims;

namespace Dkw.Abp.Security;

public static class ClaimsPrincipalExtensions
{
    public static String FullName([NotNull] this ClaimsPrincipal principal)
        => principal.Claims.FullName();
    public static String FullName([NotNull] this IEnumerable<Claim> claims)
        => $"{claims.FirstOrDefaultString(AbpClaimTypes.Name)} {claims.FirstOrDefaultString(AbpClaimTypes.SurName)}".Trim();

    public static String Email([NotNull] this ClaimsPrincipal principal)
        => principal.Claims.Email();
    public static String Email([NotNull] this IEnumerable<Claim> claims)
        => $"{claims.FirstOrDefaultString(AbpClaimTypes.Email)}";

    public static EmailAddress EmailAddress([NotNull] this ClaimsPrincipal principal)
        => principal.Claims.EmailAddress();
    public static EmailAddress EmailAddress([NotNull] this IEnumerable<Claim> claims)
        => new(claims.FullName(), claims.Email());

    public static String ZoneInfo([NotNull] this ClaimsPrincipal principal)
        => principal.Claims.ZoneInfo();
    public static String ZoneInfo(this IEnumerable<Claim> claims)
        => claims.FirstOrDefaultString(DkwClaims.ZoneInfo)
        ?? DkwDefaults.TimeZone;
}

