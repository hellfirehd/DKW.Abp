// DKW ABP Framework Extensions
// Copyright (C) 2025 Doug Wilson
//
// This program is free software: you can redistribute it and/or modify it under the terms of
// the GNU Affero General Public License as published by the Free Software Foundation, either
// version 3 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License along with this
// program. If not, see <https://www.gnu.org/licenses/>.

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
