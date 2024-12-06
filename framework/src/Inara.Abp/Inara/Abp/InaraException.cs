namespace Inara.Abp;

public class InaraException : Exception
{
    public InaraException(ErrorCode code)
    {
        ErrorCode = code;
    }

    public InaraException(ErrorCode code, String? message) : base(message)
    {
        ErrorCode = code;
    }

    public InaraException(ErrorCode code, String? message, Exception? innerException) : base(message, innerException)
    {
        ErrorCode = code;
    }

    public ErrorCode ErrorCode { get; }

    public override String ToString() => $"{GetType().Name}: {ErrorCode} {Message}";
}