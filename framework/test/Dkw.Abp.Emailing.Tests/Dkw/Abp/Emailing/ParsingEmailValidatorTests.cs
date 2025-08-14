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
