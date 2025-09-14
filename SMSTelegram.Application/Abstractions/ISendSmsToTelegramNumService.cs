using SMSTelegram.Application.Models.Sms;

namespace SMSTelegram.Application.Abstractions;

public interface ISendSmsToTelegramNumService
{
    Task HandleAsync(SendSmsCommand command, CancellationToken cancellationToken);
}