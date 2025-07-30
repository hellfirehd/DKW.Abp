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

public class EmailPropertyValidator<T>()
    : PropertyValidator<T, Email>
{
    public override String Name => "EmailValidator";

    public override Boolean IsValid(ValidationContext<T> context, Email value)
        => EmailValidator.IsValid(value);
}

public class NullableEmailPropertyValidator<T>
    : PropertyValidator<T, Email?>
{
    public override String Name => "NullableEmailValidator";

    protected override String GetDefaultMessageTemplate(String errorCode) => "Required";

    public override Boolean IsValid(ValidationContext<T> context, Email? value)
        // Null is not invalid
        => value is null || EmailValidator.IsValid(value);
}

public static partial class EmailValidatorExtensions
{
    /// <summary>
    /// Defines a validator on the current rule builder for <see cref="Email?"/> properties. Validation will fail if the value is not null and not a valid Member Number.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="ruleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<TRequest, Email?> Email<TRequest>(this IRuleBuilder<TRequest, Email?> ruleBuilder)
        => ruleBuilder.SetValidator(new NullableEmailPropertyValidator<TRequest>());

    /// <summary>
    /// Defines a validator on the current rule builder for <see cref="Email?"/> properties. Validation will fail if the value is not null and not a valid Member Number.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="ruleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<TRequest, Email> Email<TRequest>(this IRuleBuilder<TRequest, Email> ruleBuilder)
        => ruleBuilder.SetValidator(new EmailPropertyValidator<TRequest>());
}
