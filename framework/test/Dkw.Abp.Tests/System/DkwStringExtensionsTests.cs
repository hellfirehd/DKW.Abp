namespace System;

public class DkwStringExtensionsTests
{
    private const String Line1 = "The quick brown fox jumped over the lazy dog.";
    private const String Line2 = "Waltz, bad nymph, for quick jigs vex.";
    private const String Line3 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin vehicula dapibus tellus, non semper felis laoreet in.";
    private const String Line4 = "Suspendisse blandit augue urna, quis bibendum tortor iaculis sit amet. Sed sit amet lacinia nisl, et elementum nibh.";

    private const String ShortMultiLine = Line1 + "\r\n" + Line2;

    private const String LongMultiLine = Line3 + "\r\n" + Line4;

    [Theory]
    [InlineData(null!, 50, false, "")]
    [InlineData("", 50, false, "")]
    [InlineData(" ", 50, false, "")]
    [InlineData(Line1, 0, false, Line1)]
    [InlineData(Line1, 1, false, "T" + DkwStringExtensions.DefaultEllipsis)]
    [InlineData(Line1, 50, false, Line1)]

    [InlineData(ShortMultiLine, 0, true, Line1)]
    [InlineData(LongMultiLine, 0, true, Line3)]

    [InlineData(ShortMultiLine, 1, true, "T" + DkwStringExtensions.DefaultEllipsis)]
    [InlineData(LongMultiLine, 10, true, "Lorem" + DkwStringExtensions.DefaultEllipsis)]

    [InlineData(ShortMultiLine, 50, true, Line1)]
    [InlineData(LongMultiLine, 50, true, "Lorem ipsum dolor sit amet, consectetur" + DkwStringExtensions.DefaultEllipsis)]
    public void Truncate_should_work_as_expected(String? input, Int32 maxLength, Boolean firstLineOnly, String output)
    {
        input.Truncate(maxLength, firstLineOnly).ShouldBe(output.Trim());
    }

    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData(" ", "")]
    [InlineData("test@example.com", "test@example.com")]
    [InlineData("Contact us at <test@example.com>", "Contact us at")]
    [InlineData("Item: (123456), <info@example.com>, [Something Else]", "Item: , ,")]
    [InlineData("No emails here!", "No emails here!")]
    [InlineData("[4301] Carolyn Clarke () <carolynjoanclarke@gmail.com>", "Carolyn Clarke")]
    [InlineData("->", "->")]
    [InlineData("1) Numbered Point", "1) Numbered Point")]
    public void RemoveEnclosedText_should_remove_enclosured_text(String? input, String expected)
    {
        input.RemoveEnclosedText().ShouldBe(expected);
    }
}
