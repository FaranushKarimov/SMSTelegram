using SMSTelegram.Application.Abstractions;
using SMSTelegram.Application.Models.CountryCode;
using SMSTelegram.Application.Models.Sms;
using SMSTelegram.Application.Models.Users;
using SMSTelegram.Domain.Entities;
using SMSTelegram.Domain.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SMSTelegram.Application.Services;

public class SendSmsToTelegramNumService(
    IUserService userService,
    ISmsService smsService,
    ITelegramBotClient botClient,
    ICountryCodeService countryService ,
    ILoggerService logger) : ISendSmsToTelegramNumService
{
    public async Task HandleAsync(SendSmsCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.PhoneNumber) || string.IsNullOrWhiteSpace(command.MessageContent))
        {
            logger.LogMessage("BadRequest: Phone number and message cannot be empty.", "400", command.PhoneNumber);
            throw new ArgumentException("Phone number and message cannot be empty.");
        }

        var countryCode = await ExtractCountryCode(command.PhoneNumber,cancellationToken);
        if (countryCode == null)
        {
            logger.LogMessage("BadRequest: Invalid country code.", "400", command.PhoneNumber);
            throw new ArgumentException("Invalid country code.");
        }

        UserDto? user = null;
        try
        {
            user = await userService.GetByPhoneNumberAsync(command.PhoneNumber);
        }
        catch
        {
            await smsService.SendSmsAsync(command.PhoneNumber, command.MessageContent);
            logger.LogMessage(
                $"User not found in DB. SMS sent to {command.PhoneNumber} ({countryCode}).",
                "200",
                command.PhoneNumber
            );
            return;
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

    public async Task<GetCountryDto?> ExtractCountryCode(
        string phoneNumber,
        CancellationToken ct)
    {
        var digits = phoneNumber
            .Replace("+", "")
            .Replace(" ", "")
            .Replace("-", "")
            .Replace("(", "")
            .Replace(")", "");

        var response = await countryService.GetAllAsync(ct);

        if (response.Data == null || !response.Data.Any())
            return null;

        var match = response.Data
            .Where(c => digits.StartsWith(c.Code))   
            .OrderByDescending(c => c.Code.Length)  
            .FirstOrDefault();

        return match;
    }

    
    public async Task BroadcastAsync(string message, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            logger.LogMessage("BadRequest: Broadcast message cannot be empty.", "400", null);
            throw new ArgumentException("Broadcast message cannot be empty.");
        }

        var telegramUsers = await userService.GetAllUniqueTelegramUserIdsAsync(cancellationToken);

        foreach (var telegramId in telegramUsers)
        {
            try
            {
                var chatId = new ChatId(telegramId);
                await botClient.SendTextMessageAsync(chatId, message, cancellationToken: cancellationToken);

                logger.LogMessage($"Broadcast message sent to Telegram user {telegramId}.", "200", null);
            }
            catch (Exception ex)
            {
                logger.LogMessage($"Broadcast error for {telegramId}: {ex.Message}", "500", null);
            }
        }
    }
}