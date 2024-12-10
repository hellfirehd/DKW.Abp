namespace Inara.Abp;

public class InaraAbpException : Exception
{
    public InaraAbpException(InaraAbpErrorCode code)
    {
        ErrorCode = code;
    }

    public InaraAbpException(InaraAbpErrorCode code, String? message) : base(message)
    {
        ErrorCode = code;
    }

    public InaraAbpException(InaraAbpErrorCode code, String? message, Exception? innerException) : base(message, innerException)
    {
        ErrorCode = code;
    }

    public InaraAbpErrorCode ErrorCode { get; }

    public override String ToString() => $"{GetType().Name}: {ErrorCode} {Message}";
}