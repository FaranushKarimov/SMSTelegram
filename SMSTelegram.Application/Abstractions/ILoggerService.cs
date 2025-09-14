namespace SMSTelegram.Application.Abstractions;

public interface ILoggerService
{
    void LogMessage(string message, string statusCode, string phoneNumber);
}