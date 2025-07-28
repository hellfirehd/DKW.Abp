using System.Diagnostics;

namespace Dkw.Abp;

[DebuggerDisplay("[{Code}:{Message}]")]
public readonly struct ErrorCode(Int32 code, String message)
{
    public readonly Int32 Code = code;
    public readonly String Message = message;

    public override String ToString() => $"[{Code}:{Message}]";

    public static implicit operator String(ErrorCode code) => code.ToString();
}
