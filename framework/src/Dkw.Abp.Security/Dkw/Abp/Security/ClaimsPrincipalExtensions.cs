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

