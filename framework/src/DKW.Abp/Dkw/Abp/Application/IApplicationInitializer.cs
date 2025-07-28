namespace Dkw.Abp.Application;

public interface IApplicationInitializer
{
    /// <summary>
    /// Get a value that indicates when an <see cref="IApplicationInitializer"/> should run relative to other initializers. Lower values are executed before higher values.
    /// </summary>
    Int32 Priority { get; }
    Task InitializeAsync(CancellationToken cancellationToken = default);
    Boolean AbortApplicationOnException => false;
    Boolean IsEnabled { get; }
}
