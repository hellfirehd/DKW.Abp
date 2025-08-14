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

public static class LoggingTemplateDefaults
{
    public const String BootstrapTemplate = "[{Level:u3}] {Message:lj}{NewLine}{Exception}";
    public const String BrowserConsole = "[{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}";
    public const String ConsoleTemplate = "[{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}";
    public const String DetailsTemplate = "Busness Rule Violation {Code}: {Details} {Data}";
    public const String NoDetailsTemplate = "Busness Rule Violation {Code}: {Data}";
}
