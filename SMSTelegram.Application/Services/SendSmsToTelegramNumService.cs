using SMSTelegram.Application.Abstractions;
using SMSTelegram.Application.Models.Sms;
using SMSTelegram.Application.Models.Users;
using SMSTelegram.Domain.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SMSTelegram.Application.Services;

public class SendSmsToTelegramNumService(
    IUserService userService,
    ISmsService smsService,
    ITelegramBotClient botClient,
    ILoggerService logger) : ISendSmsToTelegramNumService
{
    public async Task HandleAsync(SendSmsCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.PhoneNumber) || string.IsNullOrWhiteSpace(command.MessageContent))
        {
            logger.LogMessage("BadRequest: Phone number and message cannot be empty.", "400", command.PhoneNumber);
            throw new ArgumentException("Phone number and message cannot be empty.");
        }

        var countryCode = ExtractCountryCode(command.PhoneNumber);
        if (countryCode == null)
        {
            logger.LogMessage("BadRequest: Invalid country code.", "400", command.PhoneNumber);
            throw new ArgumentException("Invalid country code.");
        }

        UserDto user;
        try
        {
            user = await userService.GetByPhoneNumberAsync(command.PhoneNumber);
        }
        catch
        {
            logger.LogMessage($"NotFound: User with phone {command.PhoneNumber} not found.", "404", command.PhoneNumber);
            throw;
        }

        try
        {
            if (string.IsNullOrEmpty(user.UserTelegramId))
            {
                await smsService.SendSmsAsync(command.PhoneNumber, command.MessageContent);
                logger.LogMessage($"SMS sent successfully to {command.PhoneNumber} ({countryCode}).", "200", command.PhoneNumber);
                return;
            }

            var chatId = new ChatId(user.UserTelegramId);
            await botClient.SendTextMessageAsync(chatId, command.MessageContent, cancellationToken: cancellationToken);
            logger.LogMessage($"Message sent successfully to Telegram user {user.UserTelegramId}.", "200", command.PhoneNumber);
        }
        catch (Exception ex)
        {
            logger.LogMessage($"Error: {ex.Message}", "500", command.PhoneNumber);
            throw;
        }
    }

    private static CountryCode? ExtractCountryCode(string phoneNumber)
    {
        phoneNumber = phoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Trim();

        if (phoneNumber.StartsWith("+"))
        {
            string digits = phoneNumber.Substring(1);
            foreach (CountryCode code in Enum.GetValues(typeof(CountryCode)))
            {
                string countryCodeStr = ((int)code).ToString();
                if (digits.StartsWith(countryCodeStr))
                    return code;
            }
        }
        else
        {
            foreach (CountryCode code in Enum.GetValues(typeof(CountryCode)))
            {
                string countryCodeStr = ((int)code).ToString();
                if (phoneNumber.StartsWith(countryCodeStr))
                    return code;
            }
        }

        return null;
    }
}