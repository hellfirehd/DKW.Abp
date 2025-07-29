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
