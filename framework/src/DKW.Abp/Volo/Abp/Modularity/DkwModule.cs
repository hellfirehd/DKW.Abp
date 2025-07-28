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

namespace Volo.Abp.Modularity;

public abstract class DkwModule<T> : AbpModule, ILoggedModule<T>
    where T : class, ILoggedModule, new()
{
    protected DkwModule()
    {
        RuntimeId = Guid.NewGuid();
        Instance = RuntimeId.ToString("N");
    }

    public Guid RuntimeId { get; }
    public String Instance { get; }
    public abstract String Endpoint { get; }
    public String Application { get; private set; } = String.Empty;
    public String Version { get; private set; } = String.Empty;

    public void SetProperties(String application, String semanticVersion)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(application);
        ArgumentException.ThrowIfNullOrWhiteSpace(semanticVersion);

        Application = application;
        Version = semanticVersion;
    }
}
