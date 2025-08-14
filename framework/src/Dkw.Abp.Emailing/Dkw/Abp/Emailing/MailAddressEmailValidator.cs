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

namespace Dkw.Abp.Emailing;

public class MailAddressEmailValidator : IEmailValidator
{
    public Boolean IsValid(String email)
    {
        try
        {
            _ = new MailAddress(email);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public Boolean IsValid(EmailAddress emailAddress)
    {
        try
        {
            _ = new MailAddress(emailAddress.Email, emailAddress.Name);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
