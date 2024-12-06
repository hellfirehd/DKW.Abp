using System.Diagnostics;
using System.Globalization;
using System.Numerics;

namespace Inara.Abp.IdGeneration.Internal;

// Copyright Talles L (https://github.com/tallesl) https://github.com/tallesl/net-36 

/// <summary>
/// Base 10 to base 36 and vice versa.
/// </summary>
[DebuggerNonUserCode]
public static class Base36
{
    private const String Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private const String Min = "-1Y2P0IJ32E8E8";

    private const String Max = "1Y2P0IJ32E8E7";

    /// <summary>
    /// Checks if the given value would cause a overflow (by being out of the long.MinValue to long.MaxValue range).
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <returns>True if the value would cause an overflow, false otherwise</returns>
    public static Boolean WouldOverflow(String value)
        => CompareInt(Min, SanitizeOrThrow(value)) < 0 || CompareInt(value, Max) < 0;

    /// <summary>
    /// Compare two specified base 36 values and returns an integer that indicates their relative position in the
    /// sort order, similar to the string.Compare method.
    /// <param name="valueA">First value of the comparison</param>
    /// <param name="valueB">Second value of the comparison</param>
    /// <returns>A integer indicating how the two values relate together</returns>
    public static Int32 Compare(String valueA, String valueB) => CompareInt(SanitizeOrThrow(valueA), SanitizeOrThrow(valueB));

    /// <summary>
    /// Converts from base 36 to base 10.
    /// </summary>
    /// <param name="value">Value to convert</param>
    /// <returns>Value in base 10</returns>
    public static Int64 Decode(String value)
    {
        value = SanitizeOrThrow(value);

        CheckOverflow(value);

        var negative = value[0] == '-';

        value = Abs(value);

        var decoded = 0L;

        for (var i = 0; i < value.Length; ++i)
        {
            decoded += Digits.IndexOf(value[i], StringComparison.InvariantCulture) * (Int64)BigInteger.Pow(Digits.Length, value.Length - i - 1);
        }

        return negative ? decoded * -1 : decoded;
    }

    /// <summary>
    /// Converts from base 10 to base 36.
    /// </summary>
    /// <param name="value">Value to convert</param>
    /// <returns>Value in base 36</returns>
    public static String Encode(Int64 value)
    {
        // hard coded value due to "Negating the minimum value of a twos complement number is invalid."
        if (value == Int64.MinValue)
        {
            return Min;
        }

        var negative = value < 0;

        value = Math.Abs(value);

        var encoded = String.Empty;

        do
        {
            encoded = Digits[(Int32)(value % Digits.Length)] + encoded;
        }
        while ((value /= Digits.Length) != 0);

        return negative ? "-" + encoded : encoded;
    }

    private static String Abs(String value) => value[0] == '-' ? value[1..] : value;

    private static void CheckOverflow(String value)
    {
        if (CompareInt(Min, value) < 0)
        {
            throw new ArgumentException(String.Format(CultureInfo.InvariantCulture,
                "Value \"{0}\" would overflow since it's less than long.MinValue.", value));
        }

        if (CompareInt(value, Max) < 0)
        {
            throw new ArgumentException(String.Format(CultureInfo.InvariantCulture,
                "Value \"{0}\" would overflow since it's greater than long.MaxValue.", value));
        }
    }

    private static String SanitizeOrThrow(String value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (String.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("An empty or whitespace string is not valid.", nameof(value));
        }

        value = value.Trim().ToUpperInvariant();

        return Abs(value).Any(c => !Digits.Contains(c, StringComparison.InvariantCulture))
            ? throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "Invalid value: \"{0}\".", value), nameof(value))
            : value;
    }

    private static Int32 CompareInt(String valueA, String valueB)
    {
        if (valueA == valueB)
        {
            return 0;
        }

        var negativeA = valueA[0] == '-';
        var negativeB = valueB[0] == '-';

        var bothNegative = negativeA && negativeB;

        if (!bothNegative && negativeA)
        {
            return 1;
        }

        if (!bothNegative && negativeB)
        {
            return -1;
        }

        valueA = Abs(valueA);
        valueB = Abs(valueB);

        if (valueA.Length < valueB.Length)
        {
            return bothNegative ? -1 : 1;
        }

        if (valueA.Length > valueB.Length)
        {
            return bothNegative ? 1 : -1;
        }

        for (var i = 0; i < valueA.Length; ++i)
        {
            var digitA = Digits.IndexOf(valueA[i], StringComparison.InvariantCulture);
            var digitB = Digits.IndexOf(valueB[i], StringComparison.InvariantCulture);

            if (digitA != digitB)
            {
                return (digitA < digitB ? 1 : -1) * (bothNegative ? -1 : 1);
            }
        }

        throw new Exception("Logic error in the library, please contact the library author.");
    }
}