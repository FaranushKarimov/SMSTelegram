namespace SMSTelegram.Application.Models.CountryCode;

public record UpdateCountryCodeDto
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; }
    public bool IsActive { get; set; }
}