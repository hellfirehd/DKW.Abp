using System.Diagnostics.CodeAnalysis;

namespace System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static Guid FirstOrDefaultGuid([NotNull] this IEnumerable<Claim> claims, String claimType, Guid defaultValue = default)
    {
        var claimOrNull = claims.FirstOrDefault(claimType);
        if (claimOrNull == null || claimOrNull.Value.IsNullOrWhiteSpace())
        {
            return defaultValue;
        }

        if (Guid.TryParse(claimOrNull.Value, out Guid value))
        {
            return value;
        }

        return defaultValue;
    }

    public static Int32 FirstOrDefaultInt32([NotNull] this IEnumerable<Claim> claims, String claimType, Int32 defaultValue = default)
    {
        var claimOrNull = claims.FirstOrDefault(claimType);
        if (claimOrNull == null || claimOrNull.Value.IsNullOrWhiteSpace())
        {
            return defaultValue;
        }

        if (Int32.TryParse(claimOrNull.Value, out Int32 value))
        {
            return value;
        }

        return defaultValue;
    }

    public static String? FirstOrDefaultString([NotNull] this IEnumerable<Claim> claims, String claimType, String? defaultValue = default)
    {
        var claimOrNull = claims.FirstOrDefault(claimType);
        if (claimOrNull == null || claimOrNull.Value.IsNullOrWhiteSpace())
        {
            return defaultValue;
        }

        return claimOrNull.Value.Trim();
    }

    public static Claim? FirstOrDefault([NotNull] this IEnumerable<Claim> claims, String claimType)
    {
        ArgumentNullException.ThrowIfNull(claims);

        return claims.FirstOrDefault(c => String.Equals(c.Type, claimType, StringComparison.OrdinalIgnoreCase));
    }
}
