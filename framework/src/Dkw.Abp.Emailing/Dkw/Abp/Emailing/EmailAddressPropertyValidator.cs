using FluentValidation;
using FluentValidation.Validators;

namespace Dkw.Abp.Emailing;

public class EmailAddressPropertyValidator<T>()
    : PropertyValidator<T, EmailAddress>
{
    public override String Name => "EmailAddressValidator";

    public override Boolean IsValid(ValidationContext<T> context, EmailAddress value)
        => EmailValidator.IsValid(value);
}

public class NullableEmailAddressPropertyValidator<T>
    : PropertyValidator<T, EmailAddress?>
{
    public override String Name => "NullableEmailAddressValidator";

    protected override String GetDefaultMessageTemplate(String errorCode) => "Required";

    public override Boolean IsValid(ValidationContext<T> context, EmailAddress? value)
        // Null is not invalid
        => value is null || EmailValidator.IsValid(value);
}

public static partial class EmailAddressValidatorExtensions
{
    /// <summary>
    /// Defines a validator on the current rule builder for <see cref="EmailAddress?"/> properties. Validation will fail if the value is not null and not a valid Member Number.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="ruleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<TRequest, EmailAddress?> EmailAddress<TRequest>(this IRuleBuilder<TRequest, EmailAddress?> ruleBuilder)
        => ruleBuilder.SetValidator(new NullableEmailAddressPropertyValidator<TRequest>());

    /// <summary>
    /// Defines a validator on the current rule builder for <see cref="EmailAddress?"/> properties. Validation will fail if the value is not null and not a valid Member Number.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="ruleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<TRequest, EmailAddress> EmailAddress<TRequest>(this IRuleBuilder<TRequest, EmailAddress> ruleBuilder)
        => ruleBuilder.SetValidator(new EmailAddressPropertyValidator<TRequest>());
}
