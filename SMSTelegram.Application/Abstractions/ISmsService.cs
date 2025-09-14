namespace SMSTelegram.Application.Abstractions;

public interface ISmsService
{
    Task SendSmsAsync(string phoneNumber, string message);
}