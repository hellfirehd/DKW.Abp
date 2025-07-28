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
