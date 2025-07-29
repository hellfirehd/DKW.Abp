// Canadian Professional Counsellors Association Application Suite
// Copyright (C) 2024 Doug Wilson
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

public class EmailComparer(EmailComparerMode mode = EmailComparerMode.LocalDomain) : Comparer<Email>, IComparer<Email?>
{
    private readonly EmailComparerMode _mode = mode;

    private record struct Parts(Int32 Local, Int32 Domain);

    public override Int32 Compare(Email x, Email y) => CompareInt(x, y);

    public Int32 Compare(Email? x, Email? y)
    {
        if (x == null)
        {
            if (y != null)
            {
                return -1;
            }

            return 0;
        }

        if (y == null)
        {
            return 1;
        }

        if (x is Email xEmail)
        {
            if (y is Email yEmail)
            {
                return CompareInt(xEmail, yEmail);
            }

            return 1;
        }

        if (y is Email)
        {
            return 1;
        }

        throw new ArgumentException("Not an Email", nameof(y));
    }

    private Int32 CompareInt(Email x, Email y) => Evaluate(GetLocalAndDomain(x, y));

    private Int32 Evaluate(Parts parts) => _mode switch
    {
        EmailComparerMode.LocalDomain => parts.Local == 0 ? parts.Domain : parts.Local,
        EmailComparerMode.DomainLocal => parts.Domain == 0 ? parts.Local : parts.Domain,
        _ => throw new InvalidOperationException("Invalid email comparer mode."),
    };

    [SuppressMessage("Roslynator", "RCS1235:Optimize method call", Justification = "Local part is case sensitive")]
    private static Parts GetLocalAndDomain(Email x, Email y)
    {
        var xParts = Split(x);

        var yParts = Split(y);

        var local = String.Compare(xParts[0], yParts[0], StringComparison.Ordinal);
        var domain = String.Compare(xParts[1], yParts[1], StringComparison.OrdinalIgnoreCase);

        return new Parts(local, domain);
    }

    private static readonly String[] EmptyParts = [String.Empty, String.Empty];
    private static String[] Split(Email x)
    {
        var xParts = x.ToString().Split('@', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (xParts.Length != 2)
        {
            return EmptyParts;
        }

        return xParts;
    }
}
