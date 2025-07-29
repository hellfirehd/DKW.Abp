using System.Text.RegularExpressions;

namespace Dkw.Abp.Emailing;

public partial class W3CEmailValidator : IEmailValidator
{
    public Boolean IsValid(String? email)
    {
        if (String.IsNullOrWhiteSpace(email) || email.Length is < 3 or >= 255)
        {
            return false;
        }

        return W3C().IsMatch(email);
    }
    public Boolean IsValid(EmailAddress emailAddress)
    {
        if (String.IsNullOrWhiteSpace(emailAddress.Name))
        {
            return false;
        }
        return IsValid(emailAddress.Email);
    }

    [GeneratedRegex(@"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$")]
    private static partial Regex W3C();
}
