using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace System;

public static partial class DkwStringExtensions
{
    public const String DefaultEllipsis = "...";

    /// <summary>
    /// Truncates <paramref name="input"/> to <paramref name="length"/> and appends <see cref="DefaultEllipsis"/> to the end.
    /// </summary>
    /// <param name="input">value to be truncated</param>
    /// <param name="length">length to return including the ellipsis (...), defaults to 100. 0 returns the original string unmodified.</param>
    /// <param name="firstLineOnly"></param>
    /// <param name="ellipsis"></param>
    public static String Truncate(this String? input, Int32 length = 100, Boolean firstLineOnly = false, String ellipsis = DefaultEllipsis)
    {
        if (String.IsNullOrWhiteSpace(input))
        {
            return String.Empty;
        }

        input = input.Trim();

        if (firstLineOnly)
        {
            var index = input.IndexOf('\n');
            if (index > 0)
            {
                input = input[..index].Trim();
            }
        }

        if (length == 0)
        {
            return input;
        }

        if (length - ellipsis.Length < 1)
        {
            length = ellipsis.Length + 1;
        }

        if (input.Length <= length - ellipsis.Length)
        {
            return input;
        }

        var truncated = input[..(length - ellipsis.Length)];
        var lastSpaceIndex = truncated.LastIndexOf(' ');

        if (lastSpaceIndex > 0)
        {
            truncated = truncated[..lastSpaceIndex];
        }

        return truncated + ellipsis;
    }

    public static String Format(this String format, params Object[] args)
        => format.Format(CultureInfo.InvariantCulture, args);

    public static String Format(this String format, IFormatProvider formatProvider, params Object[] args)
        => String.Format(formatProvider, format, args);

    public static String Quoted(this String input)
    {
        return input is null
                       ? throw new ArgumentNullException(nameof(input))
                       : input.StartsWith('"') && input.EndsWith('"') ? input : "\"" + input + "\"";
    }

    public static Guid ToGuid(this String input)
        => Guid.TryParse(input, out var guid)
            ? guid
            : throw new ArgumentException($"Don't know how to parse '{input}' into a GUID.");

    private static readonly Regex AllCaps = AllCapsRegex();

    /// <summary>
    /// Turn a pascal case or camel case string into proper case.
    /// If the input is an abbreviation, the input is return unmodified.
    /// </summary>
    /// <param name="input"></param>
    /// <example>
    /// input : HelloWorld
    /// output : Hello World
    /// </example>
    /// <example>
    /// input : BBC
    /// output : BBC
    /// </example>
    /// <example>
    /// input : IPAddress
    /// output : IP Address
    /// </example>
    [DebuggerStepThrough]
    public static String ToTitleCase(this String input)
    {
        if (input == null)
        {
            return String.Empty;
        }

        // If there are 0 or 1 characters, just return the string.
        if (input.Length < 2)
        {
            return input.ToUpper(CultureInfo.CurrentCulture);
        }

        // If the input is just an abbreviation then return the original.
        if (AllCaps.IsMatch(input))
        {
            return input;
        }

        // Start with the first character.
        var result = input[..1].ToUpper(CultureInfo.CurrentCulture);

        // Add the remaining characters.
        for (var i = 1; i < input.Length; i++)
        {
            if (Char.IsUpper(input[i]) && i + 1 < input.Length && Char.IsLower(input[i + 1]))
            {
                result += " ";
            }

            result += input[i];
        }

        return result.Trim();
    }

    /// <summary>
    /// Removes any text enclosed by (), <>, or [], including the delimiters.
    /// Handles cases where delimiters are mismatched.
    /// </summary>
    /// <param name="input">The input string that may contain enclosed text.</param>
    /// <returns>A new string with enclosed text removed. If the input is <see langword="null"/> or consists only of
    /// whitespace, an empty string is returned.</returns>
    public static String RemoveEnclosedText(this String? input)
    {
        if (String.IsNullOrWhiteSpace(input))
        {
            return String.Empty;
        }

        // Replace all matches with an empty string.
        return EnclosedText().Replace(input, String.Empty).Trim();
    }

    public static String? ToLfLineEndings(this String self)
        => self == null ? null : NewLineRegExFactory().Replace(self, "\n");

    public static String UnlessNullOrWhiteSpace(this String? input, String unless = "")
    {
        if (String.IsNullOrWhiteSpace(input))
        {
            return unless;
        }

        return input.Trim();
    }

    public static String FormatErrors(this IEnumerable<String>? errors)
    {
        if (errors is null || !errors.Any())
        {
            return String.Empty;
        }

        var sb = new StringBuilder();

        sb.AppendLine("The following errors occured:");
        sb.AppendLine();
        foreach (var error in errors)
        {
            sb.AppendLine(CultureInfo.CurrentCulture, $"\t- {error}");
        }

        return sb.ToString();
    }

    [GeneratedRegex("[0-9A-Z]+$", RegexOptions.Compiled)]
    private static partial Regex AllCapsRegex();

    [GeneratedRegex(@"\r?\n")]
    private static partial Regex NewLineRegExFactory();

    /// <summary>
    /// Regular expression to match text enclosed by (), <>, or []. Unfortunately it does not require delimiters to be matching.
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"([\(\[\<])[^\)\]\>]*[\)\]\>]", RegexOptions.Compiled)]
    private static partial Regex EnclosedText();
}
