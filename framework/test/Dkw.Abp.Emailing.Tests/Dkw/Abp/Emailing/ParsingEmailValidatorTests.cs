using Microsoft.Extensions.Options;

namespace Dkw.Abp.Emailing;

public class ParsingEmailValidatorTests : EmailValidatorTestBase
{
    public ParsingEmailValidatorTests()
    {
        var options = Options.Create(new ParsingEmailValidatorOptions());
        Validator = new ParsingEmailValidator(options);
    }

    protected override IEmailValidator Validator { get; }

    [Theory]
    [InlineData("admin@mailserver1")]
    [InlineData("example@localhost")]
    [InlineData("user@com")]
    [InlineData("user@localserver")]
    public void Should_Validate_Email_With_Top_Level_Domains(String email)
    {
        var validator = new ParsingEmailValidator(Options.Create(new ParsingEmailValidatorOptions
        {
            AllowTopLevelDomains = true
        }));

        validator.IsValid(email).ShouldBeTrue();
    }
}
