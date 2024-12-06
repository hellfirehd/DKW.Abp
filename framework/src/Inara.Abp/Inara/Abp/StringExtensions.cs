namespace Inara.Abp;

public static class StringExtensions
{
    public static String Normalize(this String value)
        => value?.Trim().ToUpperInvariant() ?? String.Empty;
}