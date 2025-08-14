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

namespace System;

public class DkwTimeSpanExtensionsTests
{
    [Fact]
    public void ToHoursAndMinutes_works_correctly()
    {
        TimeSpan.FromMinutes(30).ToHoursAndMinutes().ShouldBe("00h 30m");
        TimeSpan.FromMinutes(1689).ToHoursAndMinutes().ShouldBe("28h 09m");
        TimeSpan.FromMinutes(24 * 60).ToHoursAndMinutes().ShouldBe("24h 00m");
    }
}
