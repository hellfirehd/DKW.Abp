using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Volo.Abp;

namespace DKW.Abp.EntityFrameworkCore.Migrations;

public abstract class DbContextMigratorBase
{
    public const String ActivitySourceName = "Migrations";

    private readonly ActivitySource _activitySource = new(ActivitySourceName);

    protected abstract ILogger Logger { get; }

    public async Task TryAsync(Func<CancellationToken, Task> task, Int32 retryCount = 3, CancellationToken cancellationToken = default)
    {
        try
        {
            using var activity = _activitySource.StartActivity("Migrating Database", ActivityKind.Client);

            var sw = Stopwatch.StartNew();

            await task(cancellationToken);

            Logger.LogInformation("Migration completed in {ElapsedMilliseconds}ms", sw.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            retryCount--;

            if (retryCount <= 0)
            {
                throw;
            }

            Logger.LogWarning(ex, "{Message}: The operation will be tried {retryCount} times more.",
                ex.Message, retryCount);

            await Task.Delay(RandomHelper.GetRandom(5000, 15000), cancellationToken);

            await TryAsync(task, retryCount, cancellationToken);
        }
    }
}