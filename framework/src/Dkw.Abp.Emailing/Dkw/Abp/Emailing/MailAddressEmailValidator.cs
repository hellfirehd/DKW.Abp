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
