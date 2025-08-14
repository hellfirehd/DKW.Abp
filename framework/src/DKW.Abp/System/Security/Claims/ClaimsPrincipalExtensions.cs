// DKW ABP Framework Extensions
// Copyright (C) 2025 Doug Wilson
//
// This program is free software: you can redistribute it and/or modify it under the terms of
// the GNU Affero General Public License as published by the Free Software Foundation, either
// version 3 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License along with this
// program. If not, see <https://www.gnu.org/licenses/>.

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
