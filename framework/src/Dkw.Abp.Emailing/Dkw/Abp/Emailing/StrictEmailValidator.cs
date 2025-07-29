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
