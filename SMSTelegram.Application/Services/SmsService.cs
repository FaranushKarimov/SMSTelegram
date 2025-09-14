using Microsoft.Extensions.Options;
using SMSTelegram.Application.Abstractions;
using SMSTelegram.Application.Options;

namespace SMSTelegram.Application.Services;

public class SmsService(HttpClient httpClient, IOptions<JasminOptions> jasminOptions) : ISmsService
{
    private readonly JasminOptions _options = jasminOptions.Value;

    public async Task SendSmsAsync(string phoneNumber, string message)
    {
        Console.WriteLine($"Original Message: {message}");

        if (string.IsNullOrEmpty(_options.ApiUrl))
            throw new InvalidOperationException("Jasmin ApiUrl is not configured in appsettings.json");

        // Конвертация текста в UTF-16BE HEX
        var hexContent = ConvertToHexUtf16Be(message);
        Console.WriteLine($"Hex Content: {hexContent}");

        // Формируем итоговый URL
        var apiUrl = _options.ApiUrl
            .Replace("{phoneNumber}", Uri.EscapeDataString(phoneNumber))
            .Replace("{messageContent}", hexContent);

        Console.WriteLine($"API URL: {apiUrl}");

        // Формируем и отправляем запрос
        var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
        var response = await httpClient.SendAsync(request);

        Console.WriteLine($"Response Status Code: {response.StatusCode}");
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response Body: {responseBody}");

        response.EnsureSuccessStatusCode();
    }

    private static string ConvertToHexUtf16Be(string input)
    {
        var bytes = System.Text.Encoding.BigEndianUnicode.GetBytes(input);
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }
}