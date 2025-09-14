using SMSTelegram.Application.Abstractions;

namespace SMSTelegram.Application.Services;

public class LoggerService: ILoggerService
{
    public void LogMessage(string message, string statusCode, string phoneNumber)
    {
        var logFilePath = Path.Combine("Logs", $"{DateTime.UtcNow:yyyy-MM-dd}.log");
        if (!Directory.Exists("Logs"))
        {
            Directory.CreateDirectory("Logs");
        }

        var logEntry = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} | Phone: {phoneNumber} | Status: {statusCode} | Message: {message}{Environment.NewLine}";
        File.AppendAllText(logFilePath, logEntry);
    }
}