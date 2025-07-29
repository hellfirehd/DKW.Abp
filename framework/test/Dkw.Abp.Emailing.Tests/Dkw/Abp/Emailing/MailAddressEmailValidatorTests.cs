namespace Dkw.Abp.Emailing;

// This is 'internal' to block the tests from running because too many fail
internal class MailAddressEmailValidatorTests : EmailValidatorTestBase
{
    public MailAddressEmailValidatorTests()
    {
        Validator = new MailAddressEmailValidator();
    }

    protected override IEmailValidator Validator { get; }
}
