// Canadian Professional Counsellors Association Application Suite
// Copyright (C) 2024 Doug Wilson
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

public sealed class SelfLogOptions
{
    public const String SectionName = "SelfLog";
    public String AppName { get; set; } = SectionName;
    public String? Path { get; set; }
    public Int32 ErrorLimit { get; set; } = 10;
    public Int32 MaxLogCount { get; set; } = 31;
    public Int64 MaxLogSize { get; set; } = 1024 * 1024 * 10; // 10 MB
}
