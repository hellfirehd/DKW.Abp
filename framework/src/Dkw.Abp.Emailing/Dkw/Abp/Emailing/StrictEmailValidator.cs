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

using System.Text.RegularExpressions;

namespace Dkw.Abp.Emailing;

public partial class StrictEmailValidator : IEmailValidator
{
    public Boolean IsValid(String? email)
    {
        if (String.IsNullOrWhiteSpace(email) || email.Length is < 3 or >= 255)
        {
            return false;
        }

        return Strict().IsMatch(email);
    }

    public Boolean IsValid(EmailAddress emailAddress)
    {
        if (String.IsNullOrWhiteSpace(emailAddress.Name))
        {
            return false;
        }

        return IsValid(emailAddress.Email);
    }

    [GeneratedRegex("^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$")]
    private static partial Regex Strict();
}
