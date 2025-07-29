namespace Dkw.Abp.Emailing;

public class EmailValidator
{
    public static IEmailValidator Instance { get; private set; } = new PatternEmailValidator();

    public static Boolean IsValid(String email) => Instance.IsValid(email);

    public static Boolean IsValid(EmailAddress emailAddress) => Instance.IsValid(emailAddress);

    public static void SetValidator(IEmailValidator validator)
    {
        ArgumentNullException.ThrowIfNull(validator);

        Instance = validator;
    }
}
