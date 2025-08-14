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

public class EmailAddressTests
{
    [Fact]
    public void EmailAddress_can_be_created_with_empty_constructor()
    {
        // Arrange

        // Act
        var emailAddress = new EmailAddress();

        // Assert
        Assert.Equal(String.Empty, emailAddress.Name);
        Assert.Equal(Email.Empty, emailAddress.Email);
    }

    [Fact]
    public void EmailAddress_can_be_created_with_name_and_email()
    {
        // Arrange
        const String name = "Jane Doe";
        const String email = "jane@example.org";

        // Act
        var emailAddress = new EmailAddress(name, email);

        // Assert
        Assert.Equal(name, emailAddress.Name);
        Assert.Equal(email, emailAddress.Email);
    }

    [Fact]
    public void EmailAddress_to_string_returns_correct_format_with_name()
    {
        // Arrange
        const String name = "Jane Doe";
        const String email = "jane@example.org";
        const String expected = "Jane Doe <jane@example.org>";
        var emailAddress = new EmailAddress(name, email);

        // Act
        var result = emailAddress.ToString();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void EmailAddress_to_string_returns_correct_format_without_name()
    {
        // Arrange
        const String email = "jane@example.org";
        var emailAddress = new EmailAddress(String.Empty, email);

        // Act
        var result = emailAddress.ToString();

        // Assert
        Assert.Equal(email, result);
    }

    [Fact]
    public void EmailAddress_parse_returns_correct_email_address_when_email_is_in_angle_brackets()
    {
        // Arrange
        const String emailAddressString = "Jane Doe <jane@example.org>";
        const String expectedName = "Jane Doe";
        const String expectedEmail = "jane@example.org";

        // Act
        var emailAddress = EmailAddress.Parse(emailAddressString);

        // Assert
        Assert.Equal(expectedName, emailAddress.Name);
        Assert.Equal(expectedEmail, emailAddress.Email);
    }

    [Fact]
    public void EmailAddress_parse_returns_correct_email_address_when_name_is_in_angle_brackets()
    {
        // Arrange
        const String emailAddressString = "<Jane Doe> jane@example.org";
        const String expectedName = "Jane Doe";
        const String expectedEmail = "jane@example.org";

        // Act
        var emailAddress = EmailAddress.Parse(emailAddressString);

        // Assert
        Assert.Equal(expectedName, emailAddress.Name);
        Assert.Equal(expectedEmail, emailAddress.Email);
    }

    [Fact]
    public void EmailAddress_implicit_conversion_to_string_returns_correct_format_with_name()
    {
        // Arrange
        const String name = "Jane Doe";
        const String email = "jane@example.org";
        var expected = $"{name} <{email}>";

        // Act
        String result = new EmailAddress(name, email);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void EmailAddress_implicit_conversion_to_string_returns_correct_format_without_name()
    {
        // Arrange
        const String email = "jane@example.org";

        // Act
        String result = new EmailAddress(String.Empty, email);

        // Assert
        Assert.Equal(email, result);
    }

    [Fact]
    public void EmailAddress_implicit_conversion_from_string_returns_correct_email_address()
    {
        // Arrange
        const String emailAddressString = "Jane Doe <jane@example.org>";
        const String expectedName = "Jane Doe";
        const String expectedEmail = "jane@example.org";

        // Act
        EmailAddress emailAddress = emailAddressString;

        // Assert
        Assert.Equal(expectedName, emailAddress.Name);
        Assert.Equal(expectedEmail, emailAddress.Email);
    }
}
