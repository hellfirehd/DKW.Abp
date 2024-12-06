using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;

namespace Inara.Abp;

/// <summary>ResourceHelper</summary>
[DebuggerStepThrough]
public static class ResourceHelper
{
    private static readonly ConcurrentDictionary<String, String> Cache = new();

    /// <summary>
    /// Looks in the assembly and namespace that contains <typeparamref name="T"/> for a resource named <paramref name="resource"/> and returns it as a <seealso cref="String"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DebuggerStepThrough]
    public static String GetString<T>(String resource)
    {
        var type = typeof(T);
        return GetString(resource, type);
    }

    /// <summary>
    /// Looks in the assembly and namespace that contains <paramref name="type"/> for an embedded
    /// resource named <paramref name="resource"/> and returns it as a <seealso cref="String"/>, if found.
    /// </summary>
    /// <remarks>
    /// Strings returned by this method are cached in memory the first time they are accessed.
    /// Subsequent requests for the same <paramref name="resource"/> are returned directly from the cache.
    /// </remarks>
    [DebuggerStepThrough]
    public static String GetString(String resource, Type type)
    {
        var cachekey = type.AssemblyQualifiedName + "::" + resource;
        return Cache.GetOrAdd(cachekey, _ =>
        {
            try
            {
                var assembly = type.GetTypeInfo().Assembly
                    ?? throw new Exception("Could not load the assembly.");

                var key = type.Namespace + "." + resource;

#pragma warning disable CS8604 // Possible null reference argument. Let the catch handle it.
                using (var reader = new StreamReader(assembly.GetManifestResourceStream(key)))
                {
                    return reader.ReadToEnd();
                }
#pragma warning restore CS8604 // Possible null reference argument.
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load the resource '{resource}' from {type.AssemblyQualifiedName}.", ex);
            }
        });
    }

    /// <summary>
    /// Loads the specified manifest <paramref name="resource"/>, scoped by the namespace of the specified <typeparamref name="T"/>, from the assembly containing <typeparamref name="T"/>.
    /// </summary>
    /// <param name="resource">The case-sensitive name of the manifest resource being requested.</param>
    /// <returns>The manifest resource; or <see langword="null"/> if no resources were specified during compilation or if the resource is not visible to the caller. You are responsible for disposing the stream.</returns>
    /// <remarks>
    /// See <see cref="Assembly.GetManifestResourceStream"/> for the underlying implementation.
    /// </remarks>
    [DebuggerStepThrough]
    public static Byte[] GetBytes<T>(String resource) => GetBytes(resource, typeof(T));

    /// <summary>
    /// Loads the specified manifest <paramref name="resource"/>, scoped by the namespace of the specified <typeparamref name="T"/>, from the assembly containing <typeparamref name="T"/>.
    /// </summary>
    /// <param name="resource">The case-sensitive name of the manifest resource being requested.</param>
    /// <returns>The manifest resource; or <see langword="null"/> if no resources were specified during compilation or if the resource is not visible to the caller. You are responsible for disposing the stream.</returns>
    /// <remarks>
    /// See <see cref="Assembly.GetManifestResourceStream"/> for the underlying implementation.
    /// </remarks>
    [DebuggerStepThrough]
    public static Byte[] GetBytes(String resource, Type type)
    {
        using (var stream = GetStream(resource, type))
        {
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Loads the specified manifest <paramref name="resource"/>, scoped by the namespace of the specified <typeparamref name="T"/>, from the assembly containing <typeparamref name="T"/>.
    /// </summary>
    /// <param name="resource">The case-sensitive name of the manifest resource being requested.</param>
    /// <returns>The manifest resource; or <see langword="null"/> if no resources were specified during compilation or if the resource is not visible to the caller. You are responsible for disposing the stream.</returns>
    /// <remarks>
    /// See <see cref="Assembly.GetManifestResourceStream"/> for the underlying implementation.
    /// </remarks>
    [DebuggerStepThrough]
    public static Stream GetStream<T>(String resource)
    {
        var type = typeof(T);
        return GetStream(resource, type);
    }

    /// <summary>
    /// Loads the specified manifest <paramref name="resource"/>, scoped by the namespace of the specified <paramref name="type"/>, from the assembly containing <paramref name="type"/>. You are responsible for disposing the stream.
    /// </summary>
    /// <param name="resource">The case-sensitive name of the manifest resource being requested.</param>
    /// <param name="type">The type whose namespace is used to scope the manifest resource name.</param>
    /// <returns>The manifest resource; or <see langword="null"/> if no resources were specified during compilation or if the resource is not visible to the caller. You are responsible for disposing the stream.</returns>
    /// <remarks>
    /// See <see cref="Assembly.GetManifestResourceStream"/> for the underlying implementation.
    /// </remarks>
    [DebuggerStepThrough]
    public static Stream GetStream(String resource, Type type)
    {
        if (String.IsNullOrWhiteSpace(resource))
        {
            throw new ArgumentException($"'{nameof(resource)}' cannot be null or whitespace.", nameof(resource));
        }

        var assembly = type.GetTypeInfo().Assembly
            ?? throw new InvalidOperationException("Could not load the assembly.");

        var key = type.Namespace + "." + resource;
        try
        {
#pragma warning disable CS8603 // Possible null reference return. Let the catch handle it.
            return assembly.GetManifestResourceStream(key);
#pragma warning restore CS8603 // Possible null reference return.
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to load the resource '{resource}' from {type.AssemblyQualifiedName}.", ex);
        }
    }
}