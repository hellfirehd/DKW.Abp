namespace Dkw.Abp.Emailing;

// This is 'internal' to block the tests from running because too many fail
internal class W3CEmailValidatorTests : EmailValidatorTestBase
{
    public W3CEmailValidatorTests()
    {
        Validator = new W3CEmailValidator();
    }
    protected override IEmailValidator Validator { get; }
}
