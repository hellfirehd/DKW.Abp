namespace Dkw.Abp.Emailing;

// This is 'internal' to block the tests from running because too many fail
internal class StrictEmailValidatorTests : EmailValidatorTestBase
{
    public StrictEmailValidatorTests()
    {
        Validator = new StrictEmailValidator();
    }
    protected override IEmailValidator Validator { get; }
}
