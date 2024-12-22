namespace DKW.Abp;

public readonly struct DkwAbpErrorCode(Int32 code, String message)
{
    public readonly Int32 Code = code;
    public readonly String Message = message;

    public override String ToString() => $"[{Code}:{Message}]";
}
