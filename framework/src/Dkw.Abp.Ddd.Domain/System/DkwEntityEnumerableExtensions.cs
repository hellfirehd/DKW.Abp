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
