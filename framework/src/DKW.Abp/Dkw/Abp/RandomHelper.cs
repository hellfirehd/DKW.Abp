namespace Dkw.Abp;

/// <summary>
/// A shortcut to use <see cref="Random"/> class.
/// Also provides some useful methods.
/// </summary>
public static class RandomHelper
{
    private static readonly Random Rnd = new();

    /// <summary>
    /// Returns a random number within a specified range.
    /// </summary>
    /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
    /// <returns>
    /// A 32-bit signed integer greater than or equal to minValue and less than maxValue;
    /// that is, the range of return values includes minValue but not maxValue.
    /// If minValue equals maxValue, minValue is returned.
    /// </returns>
    public static Int32 GetRandom(Int32 minValue, Int32 maxValue)
    {
        lock (Rnd)
        {
            return Rnd.Next(minValue, maxValue);
        }
    }

    /// <summary>
    /// Returns a nonnegative random number less than the specified maximum.
    /// </summary>
    /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to zero.</param>
    /// <returns>
    /// A 32-bit signed integer greater than or equal to zero, and less than maxValue;
    /// that is, the range of return values ordinarily includes zero but not maxValue.
    /// However, if maxValue equals zero, maxValue is returned.
    /// </returns>
    public static Int32 GetRandom(Int32 maxValue)
    {
        lock (Rnd)
        {
            return Rnd.Next(maxValue);
        }
    }

    /// <summary>
    /// Returns a nonnegative random number.
    /// </summary>
    /// <returns>A 32-bit signed integer greater than or equal to zero and less than <see cref="Int32.MaxValue"/>.</returns>
    public static Int32 GetRandom()
    {
        lock (Rnd)
        {
            return Rnd.Next();
        }
    }

    /// <summary>
    /// Gets random of given objects.
    /// </summary>
    /// <typeparam name="T">Type of the objects</typeparam>
    /// <param name="objs">List of object to select a random one</param>
    public static T GetRandomOf<T>([NotNull] params T[] objs)
    {
        if (objs is null || objs.Length == 0)
        {
            throw new ArgumentNullException(nameof(objs), "The objs array must not be null or empty.");
        }

        return objs[GetRandom(0, objs.Length)];
    }

    /// <summary>
    /// Gets random item from the given list.
    /// </summary>
    /// <typeparam name="T">Type of the objects</typeparam>
    /// <param name="list">List of object to select a random one</param>
    public static T GetRandomOfList<T>([NotNull] IList<T> list)
    {
        if (list is null || list.Count == 0)
        {
            throw new ArgumentNullException(nameof(list), "The list must not be null or empty.");
        }

        return list[GetRandom(0, list.Count)];
    }

    /// <summary>
    /// Generates a randomized list from given enumerable.
    /// </summary>
    /// <typeparam name="T">Type of items in the list</typeparam>
    /// <param name="items">items</param>
    public static List<T> GenerateRandomizedItems<T>([NotNull] IEnumerable<T> items)
    {
        if (items?.Any() != true)
        {
            throw new ArgumentNullException(nameof(items), "The items must not be null or empty.");
        }

        var currentItems = new List<T>(items);
        var randomItems = new List<T>();

        while (currentItems.Count != 0)
        {
            var randomIndex = GetRandom(0, currentItems.Count);
            randomItems.Add(currentItems[randomIndex]);
            currentItems.RemoveAt(randomIndex);
        }

        return randomItems;
    }
}
