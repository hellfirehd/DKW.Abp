namespace System;

public static class DkwTimeSpanExtensions
{
    public static String ToHoursAndMinutes(this TimeSpan timeSpan)
    {
        var minutes = timeSpan.TotalMinutes % 60;
        var hours = (timeSpan.TotalMinutes - minutes) / 60;
        return $"{hours:00}h {minutes:00}m";
    }
}
