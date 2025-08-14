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

namespace Dkw.Abp.Emailing;

public interface IEmailValidator
{
    /// <summary>
    /// Determines if the <paramref name="email"/> is a valid format.
    /// </summary>
    /// <remarks>
    /// <returns><c>true</c> if the email is in a valid format; otherwise, <c>false</c>.</returns>
    /// <param name="email">An email address.</param>
    Boolean IsValid(String email);

    /// <summary>
    /// Determines if the <paramref name="emailAddress"/> is a valid format.
    /// </summary>
    /// <remarks>
    /// <para>Validates the syntax of an email address.</para>
    /// <para>If <paramref name="allowTopLevelDomains"/> is <c>true</c>, then the validator will
    /// allow addresses with top-level domains like <c>postmaster@dk</c>.</para>
    /// <para>If <paramref name="allowInternational"/> is <c>true</c>, then the validator
    /// will use the newer International Email standards for validating the email address.</para>
    /// </remarks>
    /// <returns><c>true</c> if the name and email are valid; otherwise, <c>false</c>.</returns>
    /// <param name="emailAddress">An email address.</param>
    /// <param name="allowTopLevelDomains"><c>true</c> if the validator should allow addresses at top-level domains; otherwise, <c>false</c>.</param>
    /// <param name="allowInternational"><c>true</c> if the validator should allow international characters; otherwise, <c>false</c>.</param>
    Boolean IsValid(EmailAddress emailAddress);
}
