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

using Volo.Abp.Domain.Entities;

namespace System;

public static class DkwEntityEnumerableExtensions
{
    /// <summary>
    /// Replaces the matching <see cref="IEntity"/> in <paramref name="enumerable"/> with
    /// <paramref name="replacement"/>, if one exists.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="enumerable">The list to search</param>
    /// <param name="replacement">The replacement value.</param>
    /// <returns>The list with the new <paramref name="replacement"/>, assuming a match was found.</returns>
    public static IEnumerable<TSource> Replace<TSource>(this IEnumerable<TSource> enumerable, TSource replacement)
        where TSource : IEntity
            => enumerable.Replace(i => i.GetKeys().SequenceEqual(replacement.GetKeys()), replacement);

    public static IEnumerable<TSource> Replace<TSource, TKey>(this IEnumerable<TSource> enumerable, TSource replacement)
        where TSource : IEntity<TKey>
        where TKey : IEquatable<TKey>
            => replacement is not null
                ? enumerable.Replace(i => i.Id.Equals(replacement.Id), replacement)
                : enumerable;

    public static IEnumerable<TSource> ReplaceOrAdd<TSource>(this IEnumerable<TSource> enumerable, TSource value)
        where TSource : IEntity
        => enumerable.ReplaceOrAdd(i => i.GetKeys().SequenceEqual(value.GetKeys()), value);

    public static IEnumerable<TSource> ReplaceOrAdd<TSource, TKey>(this IEnumerable<TSource> enumerable, TSource value)
        where TSource : IEntity<TKey>
        where TKey : IEquatable<TKey>
        => enumerable.ReplaceOrAdd(i => i.Id.Equals(value.Id), value);

    public static IEnumerable<TSource> Delete<TSource>(this IEnumerable<TSource> enumerable, Object?[] keys)
        where TSource : IEntity
        => enumerable.Where(i => !i.GetKeys().SequenceEqual(keys));

    public static IEnumerable<TSource> Delete<TSource, TKey>(this IEnumerable<TSource> enumerable, TKey id)
        where TSource : IEntity<TKey>
        where TKey : IEquatable<TKey>
        => enumerable.Where(i => !i.Id.Equals(id));
}
