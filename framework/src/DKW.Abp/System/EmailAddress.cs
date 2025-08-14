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

using System.Net.Mail;

namespace System;

public readonly struct EmailAddress
{
    public static readonly EmailAddress Empty;
    private static readonly Char[] AngleBrackets = ['<', '>'];

    private readonly String? _name;

    public EmailAddress(String emailAddress)
    {
        var parts = emailAddress.Split(AngleBrackets, StringSplitOptions.RemoveEmptyEntries);
        _name = parts.Length switch
        {
            0 => String.Empty,
            1 => String.Empty,
            2 => parts[0].Trim(),
            _ => throw new FormatException("Invalid email address format."),
        };

        Email = Email.Parse(parts[^1].Trim());
    }

    public EmailAddress(String name, Email email)
    {
        ArgumentNullException.ThrowIfNull(name);

        _name = name.Trim();
        Email = email;
    }

    public String Name
    {
        get => _name ?? String.Empty;
        init => _name = value;
    }

    public Email Email { get; init; }

    public override readonly String ToString()
        => String.IsNullOrWhiteSpace(_name)
            ? Email
            : $"{_name} <{Email}>";

    public static EmailAddress Parse(String emailAddress) => new(emailAddress);

    public static implicit operator String(EmailAddress emailAddress) => emailAddress.ToString();
    public static implicit operator EmailAddress(String emailAddress) => Parse(emailAddress);

    public static implicit operator MailAddress(EmailAddress emailAddress) => new(emailAddress.Email, emailAddress.Name);
    public static implicit operator EmailAddress(MailAddress mailAddress) => new(mailAddress.Address, mailAddress.DisplayName);
}
