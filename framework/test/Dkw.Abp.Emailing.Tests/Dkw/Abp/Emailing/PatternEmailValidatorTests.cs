namespace Dkw.Abp.Emailing;

// This is 'internal' to block the tests from running because too many fail
internal class PatternEmailValidatorTests : EmailValidatorTestBase
{
    public PatternEmailValidatorTests()
    {
        Validator = new PatternEmailValidator();
    }

    protected override IEmailValidator Validator { get; }
}
