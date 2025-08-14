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

using System.Diagnostics;

namespace Dkw.Abp.OpenId;

[DebuggerDisplay("{Key}:{ClientId}")]
public class OpenIdScope
{
    private String? _displayName;

    public required String Key { get; set; }
    public required String Name { get; set; }
    public String DisplayName
    {
        get => _displayName ?? Name;
        set => _displayName = value;
    }
    public String[] Resources { get; set; } = [];
}
