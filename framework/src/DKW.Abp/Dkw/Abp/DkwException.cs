namespace Dkw.Abp;

public class DkwException : Exception
{
    public DkwException(ErrorCode code)
    {
        ErrorCode = code;
    }

    public DkwException(ErrorCode code, String? message) : base(message)
    {
        ErrorCode = code;
    }

    public DkwException(ErrorCode code, String? message, Exception? innerException) : base(message, innerException)
    {
        ErrorCode = code;
    }

    public ErrorCode ErrorCode { get; }

    public override String ToString() => $"{GetType().Name}: {ErrorCode} {Message}";
}
