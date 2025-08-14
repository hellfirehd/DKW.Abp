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
using System.Diagnostics;

namespace Dkw.Abp.Emailing;

/// <summary>
/// Validates email addresses using a parsing algorithm.
/// </summary>
/// <param name="optionsAccessor"></param>
/// <remarks>
/// <para>Validates the format of an email address.</para>
/// <para>If <see cref="ParsingEmailValidatorOptions.AllowTopLevelDomains"/> is <c>true</c>, then the validator will
/// allow addresses with top-level domains like <c>postmaster@dk</c>.</para>
/// <para>If <see cref="ParsingEmailValidatorOptions.AllowInternational"/> is <c>true</c>, then the validator
/// will use the newer International Email standards for validating the email address.</para>
/// </remarks>
public class ParsingEmailValidator(IOptions<ParsingEmailValidatorOptions> optionsAccessor) : IEmailValidator
{
    private const String AtomCharacters = "~`!#$%^&*'-_=+/?{|}";
    private const String Specials = "()<>[]:;@\\,.\"";
    private const Char Dot = '.';
    private const Char At = '@';

    protected ParsingEmailValidatorOptions Options { get; } = optionsAccessor.Value;

    [DebuggerStepThrough]
    private sealed class Context
    {
        internal Context(ParsingEmailValidatorOptions options, String email)
        {
            Email = email;
            AllowLocalDomain = options.AllowTopLevelDomains;
            AllowInternational = options.AllowInternational;
            //Length = Email.Length;

            Update();
        }

        internal String Email { get; }

        internal Boolean AllowLocalDomain { get; }
        internal Boolean AllowInternational { get; }
        internal Int32 Index { get; private set; }

        internal Boolean InEscaped { get; private set; }
        internal Boolean InDQuotes { get; private set; }
        internal Char C => Email[Index];
        internal Char Previous => Index < 1 ? Char.MinValue : Email[Index - 1];
        internal Int32 End => Email.Length - 1;
        internal String Remaining => Email[Index..];

        public override String ToString()
            => $"<{Email}> Index: {Index} Char: '{Email[Index]}'{(InDQuotes ? " InDQuotes" : "")}{(InEscaped ? " InEscaped" : "")}";

        internal Int32 Inc(Int32 amount = 1)
        {
            for (var i = 0; i < amount; i++)
            {
                Index++;
                Update();
            }

            return Index;
        }

        private void Update()
        {
            if (IsDQuote)
            {
                InDQuotes = true;
            }

            if (IsEscape)
            {
                InEscaped = true;
            }
        }

        internal void SetInEscaped(Boolean escaped = false) => InEscaped = escaped;

        internal void SetInQuoted(Boolean quoted = false) => InDQuotes = quoted;

        internal Boolean IsDot => Email[Index] == Dot;
        internal Boolean IsAt => Email[Index] == At;

        internal Boolean IsAtom => Email[Index] < 128
            ? IsLetterOrDigit || AtomCharacters.Contains(Email[Index], StringComparison.Ordinal)
            : AllowInternational;

        internal Boolean IsLetterOrDigit => IsLetter || IsDigit;
        internal Boolean IsLetter => Email[Index] is >= 'A' and <= 'Z' or >= 'a' and <= 'z';
        internal Boolean IsDigit => Email[Index] is >= '0' and <= '9';
        internal Boolean IsEscape => Email[Index] == '\\';
        internal Boolean IsDQuote => Email[Index] == '"';
        internal Boolean IsSpecial => Specials.Contains(Email[Index]);

        public Boolean IsLast => Index >= End;
    }

    /// <inheritdoc />
    public Boolean IsValid(String? email)
    {
        if (email is null || email.Length is < 3 or >= 255)
        {
            return false;
        }

        var ctx = new Context(Options, email);

        while (!ctx.IsLast)
        {
            if (ctx.InDQuotes)
            {
                if (!SkipQuoted(ctx))
                {
                    return false;
                }
            }
            else if (ctx.IsAtom)
            {
                if (!SkipAtom(ctx))
                {
                    return false;
                }
            }
            else if (ctx.IsDot)
            {
                if (ctx.Previous == Dot)
                {
                    return false;
                }

                ctx.Inc();
            }
            else if (ctx.IsAt)
            {
                if (ctx.Previous == Dot)
                {
                    return false;
                }

                break;
            }
            else if (ctx.IsSpecial)
            {
                if (!SkipSpecial(ctx))
                {
                    return false;
                }
            }
            else if (ctx.IsEscape)
            {
                if (!SkipEscaped(ctx))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        if (ctx.C != '@' || ctx.IsLast || ctx.Index > 64)
        {
            return false;
        }

        // Skip over @ literal
        ctx.Inc();

        if (ctx.C == '[')
        {
            return SkipIPv6(ctx);
        }

        var result = Uri.CheckHostName(ctx.Remaining);

        return result is UriHostNameType.Dns && (ctx.Remaining.Contains(Dot) || ctx.AllowLocalDomain)
            || result is UriHostNameType.IPv4;
    }

    public Boolean IsValid(EmailAddress emailAddress)
    {
        if (String.IsNullOrWhiteSpace(emailAddress.Name))
        {
            return false;
        }

        return IsValid(emailAddress.Email);
    }

    private static Boolean SkipIPv6(Context ctx)
    {
        // We are in IPv6 country... Skip over [ literal
        ctx.Inc();

        // we need at least 8 more characters
        if (ctx.Index + 8 >= ctx.End)
        {
            return false;
        }

        if (ctx.Remaining.StartsWith("ipv6:", StringComparison.OrdinalIgnoreCase))
        {
            // Skip over leading IPv6: literals
            ctx.Inc(5);

            var ip = ctx.Remaining[..^1];
            if (Uri.CheckHostName(ip) == UriHostNameType.IPv6)
            {
                // Skip remaining digits
                ctx.Inc(ip.Length);

                return ctx.C == ']' && ctx.IsLast;
            }
        }

        return false;
    }

    private static Boolean SkipQuoted(Context ctx)
    {
        if (ctx.Index == 0 || ctx.Previous == Dot)
        {
            // skip over leading '"'
            ctx.Inc();

            while (!ctx.IsLast)
            {
                if (ctx.C >= 128 && !ctx.AllowInternational)
                {
                    return false;
                }

                if (ctx.IsEscape && !SkipEscaped(ctx))
                {
                    return false;
                }
                else if (ctx.IsDQuote)
                {
                    ctx.SetInQuoted(false);
                    // skip over trailing " literal
                    ctx.Inc();
                    return true;
                }

                ctx.Inc();
            }
        }

        return false;
    }

    private static Boolean SkipAtom(Context ctx)
    {
        var startIndex = ctx.Index;

        while (!ctx.IsLast && ctx.IsAtom)
        {
            ctx.Inc();
        }

        return ctx.Index > startIndex;
    }

    private static Boolean SkipSpecial(Context ctx)
    {
        var startIndex = ctx.Index;

        while (!ctx.IsLast && ctx.IsSpecial && ctx.InDQuotes)
        {
            ctx.Inc();
        }

        return ctx.Index > startIndex;
    }

    private static Boolean SkipEscaped(Context ctx)
    {
        // Handles consecutive escaped characters automatically
        do
        {
            // skip over \ literal
            ctx.Inc();

            ctx.SetInEscaped(false);

            // Skip over the escapee
            ctx.Inc();
        } while (ctx.IsEscape);

        return true;
    }
}
