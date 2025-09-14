namespace SMSTelegram.Application.Models.Sms;

public record SendSmsCommand(string PhoneNumber, string MessageContent);