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

namespace Dkw.Abp.Logging;

public static class LogHeaders
{
    public const String AccountsInstanceHeader = "x-dkw-accounts-instance";
    public const String AccountsVersionHeader = "x-dkw-accounts-version";
    public const String ApiInstanceHeader = "x-dkw-api-instance";
    public const String ApiVersionHeader = "x-dkw-api-version";
    public const String ClientInstanceHeader = "x-dkw-client-instance";
    public const String ClientVersionHeader = "x-dkw-client-version";
    public const String UserAgent = "user-agent";
}
