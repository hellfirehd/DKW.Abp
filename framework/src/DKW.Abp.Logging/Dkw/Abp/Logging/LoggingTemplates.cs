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

public static class LoggingTemplates
{
    public static String BootstrapTemplate { get; set; } = LoggingTemplateDefaults.BootstrapTemplate;
    public static String ConsoleTemplate { get; set; } = LoggingTemplateDefaults.ConsoleTemplate;
    public static String DetailsTemplate { get; set; } = LoggingTemplateDefaults.DetailsTemplate;
    public static String NoDetailsTemplate { get; set; } = LoggingTemplateDefaults.NoDetailsTemplate;
    public static String BrowserConsole { get; set; } = LoggingTemplateDefaults.BrowserConsole;
}
