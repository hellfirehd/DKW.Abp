using System.Security.Cryptography;
using Volo.Abp.DependencyInjection;

namespace Inara.Abp.IdGeneration;

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