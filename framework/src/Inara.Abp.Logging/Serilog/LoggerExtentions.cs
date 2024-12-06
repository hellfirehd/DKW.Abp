using Microsoft.Extensions.Logging;
using Serilog.Events;
using Volo.Abp;

namespace Serilog;

public static class LoggerExtentions
{
    private const String Separator = ", ";
    private const String DetailsTemplate = "Busness Rule Violation {Code}: {Details} {Data}";
    private const String NoDetailsTemplate = "Busness Rule Violation {Code}: {Data}";

    public static void LogBusinessException(this ILogger logger, BusinessException exception, LogLevel logLevel = LogLevel.Warning)
    {
        if (String.IsNullOrWhiteSpace(exception.Details))
        {
            logger.Write(Translate(logLevel), exception, NoDetailsTemplate,
            exception.Code, GetData(exception));
        }
        else
        {
            logger.Write(Translate(logLevel), exception, DetailsTemplate,
                exception.Code, exception.Details, GetData(exception));
        }
    }

    private static LogEventLevel Translate(LogLevel level)
    {
        switch (level)
        {
            case LogLevel.Debug:
                return LogEventLevel.Debug;
            case LogLevel.Information:
                return LogEventLevel.Information;
            case LogLevel.Warning:
                return LogEventLevel.Warning;
            case LogLevel.Error:
                return LogEventLevel.Error;
            case LogLevel.Critical:
                return LogEventLevel.Fatal;
            case LogLevel.Trace:
                break;
            case LogLevel.None:
                return LogEventLevel.Information;
        }

        return LogEventLevel.Information;
    }

    private static String GetData(BusinessException exception)
    {
        if (exception.Data.Count > 0)
        {
            var data = new List<String>();
            foreach (var k in exception.Data.Keys)
            {
                data.Add($"{k}: {exception.Data[k]}");
            }

            return String.Join(Separator, data);
        }

        return String.Empty;
    }
}