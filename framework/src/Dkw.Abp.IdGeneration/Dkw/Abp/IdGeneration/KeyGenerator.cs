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

using System.Security.Cryptography;
using Volo.Abp.DependencyInjection;

namespace Dkw.Abp.IdGeneration;

public sealed class KeyGenerator : IKeyGenerator, IDisposable, ITransientDependency
{
    private static readonly RandomNumberGenerator RNG = RandomNumberGenerator.Create();

    // Intentionally not used: a-z
    private readonly Char[] _chars = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

    private Boolean _disposed;

    public String Generate(Int32 length)
    {
        var buffer = new Char[length];
        var bytes = new Byte[4];
        RNG.GetBytes(bytes);
        var random = new Random(BitConverter.ToInt32(bytes, 0));

        for (var i = 0; i < length; i++)
        {
            var offset = random.Next(0, _chars.Length);
            buffer[i] = _chars[offset];
        }

        return new String(buffer);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(Boolean disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                RNG.Dispose();
            }

            _disposed = true;
        }
    }
}
