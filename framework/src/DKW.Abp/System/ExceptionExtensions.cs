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

namespace System;
public static class ExceptionExtensions
{
    public static String ForLog(this Exception exception)
        => exception.DescendantsAndSelf().Concatenate();

    public static IEnumerable<Exception> DescendantsAndSelf(this Exception exception)
    {
        do
        {
            yield return exception;

            exception = exception.InnerException!;

        } while (exception is not null);
    }

    public static String Concatenate(this IEnumerable<Exception> exceptions)
    {
        var messages = exceptions.Select((e, i) => $"{i + 1}: {e.GetType().Name} - {e.Message}");

        return String.Join(Environment.NewLine, messages);
    }
}
