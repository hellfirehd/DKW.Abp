namespace System;
public static class ExceptionExtensions
{
    public static String ForLog(this Exception exception)
        => exception.DescendantsAndSelf().Concatenate();

    public static IEnumerable<Exception> DescendantsAndSelf(this Exception exception)
    {
        do
        {
            yield return exception;

            exception = exception.InnerException!;

        } while (exception is not null);
    }

    public static String Concatenate(this IEnumerable<Exception> exceptions)
    {
        var messages = exceptions.Select((e, i) => $"{i + 1}: {e.GetType().Name} - {e.Message}");

        return String.Join(Environment.NewLine, messages);
    }
}