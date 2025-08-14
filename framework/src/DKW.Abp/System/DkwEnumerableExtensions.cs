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

public static class DkwEnumerableExtensions
{
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(collection);

        ArgumentNullException.ThrowIfNull(action);

        foreach (var item in collection)
        {
            action(item);
        }

        return collection;
    }

    public const String DefaultSeparator = "\r\n";

    /// <summary>
    /// Converts a collection of objects to a [separator]-delimited string.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="separator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static String ToSeparatedString<T>(this IEnumerable<T> source, String separator = DefaultSeparator)
        => String.Join(separator, source.ToArray());

    // https://stackoverflow.com/a/41384214/32588

    public static IEnumerable<T> Insert<T>(this IEnumerable<T> enumerable, Int32 index, T value)
        => enumerable.SelectMany((x, i) => index == i ? new T[] { value, x } : [x]);

    public static IEnumerable<T> Replace<T>(this IEnumerable<T> enumerable, Int32 index, T value)
        => enumerable.Select((x, i) => index == i ? value : x);

    /// <summary>
    /// Replaces any element that matches <paramref name="predicate"/> in <paramref name="enumerable"/> with
    /// <paramref name="value"/>.
    /// </summary>
    public static IEnumerable<TSource> Replace<TSource>(this IEnumerable<TSource> enumerable, Func<TSource, Boolean> predicate, TSource value)
        => enumerable.Select((x, _) => predicate.Invoke(x) ? value : x);

    public static IEnumerable<TSource> ReplaceOrAdd<TSource>(this IEnumerable<TSource> enumerable, Func<TSource, Boolean> predicate, TSource value)
    {
        var replaced = false;
        foreach (var x in enumerable)
        {
            if (predicate.Invoke(x))
            {
                replaced = true;
                yield return value;
            }
            else
            {
                yield return x;
            }
        }

        if (replaced)
        {
            yield break;
        }

        yield return value;
    }

    public static Int32 IndexOfFirst<T>(this IEnumerable<T> enumerable, Func<T, Boolean> predicate)
    {
        var count = 0;

        foreach (var item in enumerable)
        {
            if (predicate(item))
            {
                return count;
            }

            count++;
        }

        return -1;
    }
}
