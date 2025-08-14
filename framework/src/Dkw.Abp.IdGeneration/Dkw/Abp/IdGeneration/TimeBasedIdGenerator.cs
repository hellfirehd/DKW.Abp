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

using Dkw.Abp.IdGeneration.Internal;
using Volo.Abp.DependencyInjection;

namespace Dkw.Abp.IdGeneration;

/// <summary>
/// Generates unique IDs based on values from <see cref="IClock.GetCurrentInstant"/>. This class should be registered as a Singleton.
/// </summary>
/// <remarks>
/// <para>Generated IDs are only unique within the instance.</para>
/// <para>If you want to decode the generated ID into an <see cref="Instant"/> then use <see cref="Decode(String)"/>.</para>
/// </remarks>
public class TimeBasedIdGenerator(TimeProvider clock) : IIdGenerator, ITransientDependency
{
    private readonly Lock _syncRoot = new();
    private readonly TimeProvider _clock = clock;
    private Int64 _lastTickCount;

    /// <summary>
    /// Generates unique IDs based on values from <see cref="IClock.GetCurrentInstant"/>.
    /// </summary>
    /// <returns>a <see cref="String"/> containing a unique ID</returns>.
    /// <remarks>
    /// <para>Generated IDs are only unique within the instance.</para>
    /// <para>If you want to decode the generated ID into an <see cref="Instant"/> then use <see cref="Decode(String)"/>.</para>
    /// </remarks>
    public String NextId()
    {
        lock (_syncRoot)
        {
            var currentTicksCount = _clock.GetUtcNow().ToUnixTimeMilliseconds();
            if (_lastTickCount < currentTicksCount)
            {
                Interlocked.Exchange(ref _lastTickCount, currentTicksCount);
            }
            else if (_lastTickCount >= currentTicksCount)
            {
                Interlocked.Exchange(ref _lastTickCount, _lastTickCount + 1);
            }

            return Base36.Encode(_lastTickCount);
        }
    }

    public static DateTimeOffset Decode(String timeBasedId)
        => DateTimeOffset.FromUnixTimeMilliseconds(Base36.Decode(timeBasedId));
}
