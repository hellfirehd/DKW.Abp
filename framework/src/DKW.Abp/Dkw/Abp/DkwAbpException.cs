namespace DKW.Abp;

public class DkwAbpException : Exception
{
    public DkwAbpException(DkwAbpErrorCode code)
    {
        ErrorCode = code;
    }

    public DkwAbpException(DkwAbpErrorCode code, String? message) : base(message)
    {
        ErrorCode = code;
    }

    public DkwAbpException(DkwAbpErrorCode code, String? message, Exception? innerException) : base(message, innerException)
    {
        ErrorCode = code;
    }

    public DkwAbpErrorCode ErrorCode { get; }

    public override String ToString() => $"{GetType().Name}: {ErrorCode} {Message}";
}
