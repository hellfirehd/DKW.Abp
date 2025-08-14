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

namespace Dkw.Abp.Emailing;

public abstract class EmailValidatorTestBase
{
    protected abstract IEmailValidator Validator { get; }

    [Theory]
    [InlineData("test@example.org")]
    [InlineData("prettyandsimple@example.org")]
    [InlineData("very.common@example.org")]
    [InlineData("disposable.style.email.with+symbol@example.org")]
    [InlineData("other.email-with-dash@example.org")]
    [InlineData("x@example.org")]
    [InlineData("\"much.more unusual\"@example.org")]
    [InlineData("\"very.unusual.@.unusual.com\"@example.org")]
    [InlineData("\"very.(),:;<>[]\\\".VERY.\\\"very@\\ \\\"very\\\".unusual\"@strange.example.org")]
    [InlineData("example-indeed@strange-example.org")]
    [InlineData("\"()<>[]:,;@\\\"!#$%&'-/\\=?^_`{}| ~.a\"@example.org")]
    [InlineData("\" \"@example.org")]
    [InlineData("\" \".\" \"@example.org")]
    [InlineData("example@s.solutions")]
    [InlineData("~`!#$%^&*'-_=+/?{|}@gmail.com")] // WTF!?
    [InlineData("user@[IPv6:2001:db8::1]")]
    [InlineData("user@[IPv6:2001:db8:1ff::a0b:dbd0]")]
    [InlineData("user@192.168.1.1")]
    public void Valid(String email)
    {
        Validator.IsValid(email).ShouldBeTrue();
    }

    [Theory]
    [InlineData("John\\ Smith@example.org")] // This one could go either way. The space is escaped, but it is not in a quoted string.
    [InlineData("Abc\\@def@example.org")] // Contains escaped special in the local part but is not quoted.
    [InlineData("A@b@c@example.org")] // Contains unescaped special in the local part but is not quoted.
    [InlineData("Abc.example.org")] // Missing @
    [InlineData("a\"b(c)d,e:f;gi[j\\k]l@example.org")] // None of the special characters in this local part are allowed outside quotation marks
    [InlineData("example@localhost")] // Invalid unless allowLocal == true
    [InlineData("hannah.b.mffs@@gmail.com")] // Oops, double @
    [InlineData("just\"not\"right@example.org")] // quoted strings must be dot separated or the only element making up the local part
    [InlineData("this\"is not\\allowed@example.org")] // spaces, quotes, and backslashes may only exist when within quoted strings and preceded by a backslash
    [InlineData("this\\ still\\\"not\\allowed@example.org")] // even if escaped spaces, quotes, and backslashes must still be contained by quotes
    [InlineData("user@[192.168.1.1]")]
    [InlineData("user@[IPv6:2001:db8:::1]")] // Too many consecutive colons
    [InlineData("user@[IPv6:2001:db8:1ff::a0b:dbd0")] // Missing trailing ] literal.
    public void Invalid(String email)
    {
        Validator.IsValid(email).ShouldBeFalse();
    }

    [Fact]
    public void Consecutive_dots_in_domain_should_return_False()
    {
        Validator.IsValid("test@example..com").ShouldBeFalse();
    }

    [Fact]
    public void Consecutive_dots_in_local_part_should_return_False()
    {
        // caveat: Gmail lets this next one through
        Validator.IsValid("test..user@example.org").ShouldBeFalse();
    }

    [Fact]
    public void Empty_Email_should_return_False()
    {
        Validator.IsValid(String.Empty).ShouldBeFalse();
    }

    [Fact]
    public void Invalid_LocalPart_Valid_Domain_Email_should_return_False()
    {
        Validator.IsValid("test@.com").ShouldBeFalse();
    }

    [Fact]
    public void Local_part_ending_with_a_dot_should_return_False()
    {
        Validator.IsValid("test.@example.org").ShouldBeFalse();
    }

    [Fact]
    public void Local_part_starting_with_a_dot_should_return_False()
    {
        Validator.IsValid(".test@example..com").ShouldBeFalse();
    }

    [Fact]
    public void Local_part_with_plus_addressing_should_return_True()
    {
        Validator.IsValid("john+doe@gmail.com").ShouldBeTrue();
    }

    [Fact]
    public void Local_part_quoted_with_escaped_special_char_should_return_True()
    {
        Validator.IsValid("\"a\\@b\"@example.org").ShouldBeTrue();
    }

    [Fact]
    public void Non_Email_should_return_False()
    {
        Validator.IsValid("invalid_email").ShouldBeFalse();
    }

    [Fact]
    public void Null_Email_should_return_False()
    {
        Validator.IsValid((String)null!).ShouldBeFalse();
    }

    [Fact]
    public void Quoted_special_characters_should_return_True()
    {
        Validator.IsValid("\"test!\"@example.org").ShouldBeTrue();
    }

    [Fact]
    public void Unquoted_special_characters_should_return_False()
    {
        Validator.IsValid("te,st@example.org").ShouldBeFalse();
    }

    [Fact]
    public void Valid_LocalPart_Invalid_Domain_Email_should_return_False()
    {
        Validator.IsValid("test@example!").ShouldBeFalse();
    }

    [Fact]
    public void Valid_LocalPart_Valid_Domain_Email_should_return_True()
    {
        Validator.IsValid("john.doe@gmail.com").ShouldBeTrue();
    }

    [Fact]
    public void WhiteSpace_Email_should_return_False()
    {
        Validator.IsValid("  ").ShouldBeFalse();
    }
}
