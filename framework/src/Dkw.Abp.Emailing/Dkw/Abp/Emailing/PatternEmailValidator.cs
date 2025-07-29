using System.Text.RegularExpressions;

namespace Dkw.Abp.Emailing;

public partial class PatternEmailValidator : IEmailValidator
{
    public Boolean IsValid(String? email)
    {
        if (String.IsNullOrWhiteSpace(email) || email.Length is < 3 or >= 255)
        {
            return false;
        }

        return GeneralEmail().IsMatch(email);
    }

    public Boolean IsValid(EmailAddress emailAddress)
    {
        if (String.IsNullOrWhiteSpace(emailAddress.Name))
        {
            return false;
        }

        return IsValid(emailAddress.Email);
    }

    [GeneratedRegex("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex GeneralEmail();
}
